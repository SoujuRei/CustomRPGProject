using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using CustomProjectRPG.Interfaces;

namespace CustomProjectRPG
{
    

    

    public class UIManager : ISerializeJSON<UIManager>, IUIManager
    {
        private string _lastCorruptionDisplay;
        private Dictionary<string, bool> _lastQuestDisplay; // questId -> isComplete
        private string _lastDialogueDisplay;
        private bool _isVisible;

        public event Action<string> UIRefreshed; // Event for UI refresh notifications

        
        // Initializes a new UIManager with default display states.
        
        public UIManager()
        {
            _lastCorruptionDisplay = "No corruption data.";
            _lastQuestDisplay = new Dictionary<string, bool>();
            _lastDialogueDisplay = "No dialogue available.";
            _isVisible = true;
        }

        
        // Updates the corruption display based on entity state.
        
        public void UpdateCorruptionDisplay(string entityId, CustomProjectRPG.Entities.Enum.Enum.CorruptionState state)
        {
            _lastCorruptionDisplay = $"Entity {entityId} Corruption State: {state}";
            Render();
            UIRefreshed?.Invoke(_lastCorruptionDisplay);
        }

        
        // Updates the quest display based on quest status.
        
        public void UpdateQuestDisplay(string questId, bool isComplete)
        {
            _lastQuestDisplay[questId] = isComplete;
            _lastQuestDisplay.TryGetValue(questId, out bool currentState);
            _lastQuestDisplay[questId] = currentState; // Ensure entry exists
            Render();
            UIRefreshed?.Invoke($"Quest {questId} Status: {(isComplete ? "Complete" : "In Progress")}");
        }

        
        // Updates the dialogue display with new text.
        
        public void UpdateDialogueDisplay(string dialogue)
        {
            _lastDialogueDisplay = dialogue;
            Render();
            UIRefreshed?.Invoke(_lastDialogueDisplay);
        }

        
        // Toggles UI visibility.
        
        public void ToggleVisibility()
        {
            _isVisible = !_isVisible;
            Render();
        }

        
        // Renders the current UI state to the console (placeholder for graphical UI).
        
        private void Render()
        {
            if (!_isVisible) return;
            Console.Clear();
            Console.WriteLine("=== Game UI ===");
            Console.WriteLine(_lastCorruptionDisplay);
            Console.WriteLine("Quests:");
            foreach (var quest in _lastQuestDisplay)
            {
                Console.WriteLine($"  {quest.Key}: {(quest.Value ? "Complete" : "In Progress")}");
            }
            Console.WriteLine("Dialogue:");
            Console.WriteLine($"  {_lastDialogueDisplay}");
            Console.WriteLine("===============");
        }

        
       
        
        public string Serialize()
        {
            var data = new UIData
            {
                LastCorruptionDisplay = _lastCorruptionDisplay,
                LastQuestDisplay = _lastQuestDisplay,
                LastDialogueDisplay = _lastDialogueDisplay,
                IsVisible = _isVisible
            };
            return JsonSerializer.Serialize(data);
        }

        
        
        
        public void Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<UIData>(json)
                ?? throw new JsonException("Invalid JSON format for UI state.");
            _lastCorruptionDisplay = data.LastCorruptionDisplay;
            _lastQuestDisplay = data.LastQuestDisplay ?? new Dictionary<string, bool>();
            _lastDialogueDisplay = data.LastDialogueDisplay;
            _isVisible = data.IsVisible;
            Render();
        }

        private class UIData
        {
            [JsonPropertyName("lastCorruptionDisplay")]
            public string LastCorruptionDisplay { get; set; }

            [JsonPropertyName("lastQuestDisplay")]
            public Dictionary<string, bool> LastQuestDisplay { get; set; }

            [JsonPropertyName("lastDialogueDisplay")]
            public string LastDialogueDisplay { get; set; }

            [JsonPropertyName("isVisible")]
            public bool IsVisible { get; set; }
        }
    }
}
