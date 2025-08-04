using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG.Dialogue
{

    public class DialogueManager : ISerializeJSON<DialogueManager>
    {
        private Dictionary<string, Dictionary<string, string>> _npcDialogues; // NPC ID -> Dialogue ID -> Text
        private ICorruption _corruption;
        private string _currentNpcId;
        private string _currentDialogueId;

        public event Action<string> DialogueChanged; // Observer pattern for UI updates


        /// Initializes a new DialogueManager with a corruption reference and loads dialogues from JSON.

        public DialogueManager(ICorruption corruption, string jsonDialogues = null)
        {
            _corruption = corruption ?? throw new ArgumentNullException(nameof(corruption));
            _npcDialogues = new Dictionary<string, Dictionary<string, string>>();
            _currentNpcId = string.Empty;
            _currentDialogueId = string.Empty;

            if (!string.IsNullOrEmpty(jsonDialogues))
            {
                LoadDialoguesFromJson(jsonDialogues);
            }
        }


        /// Loads dialogue data from a JSON string, organized by NPC ID.

        private void LoadDialoguesFromJson(string json)
        {
            var dialogues = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json)
                ?? throw new JsonException("Invalid JSON format for dialogues.");
            _npcDialogues = dialogues;
        }


        /// Retrieves available dialogue options for the current NPC based on corruption state.

        public List<string> GetDialogueOptions()
        {
            if (!_npcDialogues.ContainsKey(_currentNpcId) || !_npcDialogues[_currentNpcId].Any())
            {
                return new List<string> { "No dialogue available for this NPC." };
            }

            var options = new List<string>();
            var npcDialogues = _npcDialogues[_currentNpcId];

            if (npcDialogues.TryGetValue("normal", out var normalDialogue))
            {
                options.Add(normalDialogue);
            }
            if (_corruption.IsDialogueCorrupted() && npcDialogues.TryGetValue("corrupted", out var corruptedDialogue))
            {
                options.Add(corruptedDialogue);
            }
            if (_corruption.IsNoResponse() && npcDialogues.TryGetValue("noResponse", out var noResponseDialogue))
            {
                options.Clear();
                options.Add(noResponseDialogue);
            }

            return options.Count > 0 ? options : new List<string> { "No valid dialogue options." };
        }


        /// Sets and displays the current dialogue for the specified NPC, triggering an event.

        public void ShowDialogue(string npcId, string dialogueId)
        {
            if (!_npcDialogues.ContainsKey(npcId) || !_npcDialogues[npcId].ContainsKey(dialogueId))
            {
                throw new ArgumentException($"Invalid NPC ID or dialogue ID: {npcId}/{dialogueId}.", nameof(dialogueId));
            }
            _currentNpcId = npcId;
            _currentDialogueId = dialogueId;
            OnDialogueChanged();
        }


        /// Gets the current dialogue text for the active NPC.

        public string GetCurrentDialogue()
        {
            if (string.IsNullOrEmpty(_currentNpcId) || string.IsNullOrEmpty(_currentDialogueId) ||
                !_npcDialogues.ContainsKey(_currentNpcId) || !_npcDialogues[_currentNpcId].ContainsKey(_currentDialogueId))
            {
                return "No dialogue selected.";
            }
            return _npcDialogues[_currentNpcId][_currentDialogueId];
        }


        /// Serializes the current dialogue state to JSON.

        public string Serialize()
        {
            var data = new DialogueData
            {
                CurrentNpcId = _currentNpcId,
                CurrentDialogueId = _currentDialogueId
            };
            return JsonSerializer.Serialize(data);
        }


        /// Deserializes the dialogue state from JSON.

        public void Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<DialogueData>(json)
                ?? throw new JsonException("Invalid JSON format for dialogue state.");
            if (_npcDialogues.ContainsKey(data.CurrentNpcId) && _npcDialogues[data.CurrentNpcId].ContainsKey(data.CurrentDialogueId))
            {
                _currentNpcId = data.CurrentNpcId;
                _currentDialogueId = data.CurrentDialogueId;
            }
        }

        private void OnDialogueChanged()
        {
            DialogueChanged?.Invoke(GetCurrentDialogue());
        }

        private class DialogueData
        {
            [JsonPropertyName("currentNpcId")]
            public string CurrentNpcId { get; set; }

            [JsonPropertyName("currentDialogueId")]
            public string CurrentDialogueId { get; set; }
        }
    }
}
