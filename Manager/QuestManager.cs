using CustomProjectRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG.Manager
{
    public class QuestManager : ISerializeJSON<QuestManager>, IQuestManager
    {
        private Dictionary<string, QuestData> _quests; // Quest ID -> QuestData
        private ICorruption _corruption; // For favor-based quest adjustments

        public event Action<string, bool> QuestUpdated; // Event for quest status changes (questId, isComplete)

       
        /// Initializes a new QuestManager with a corruption reference.
       
        public QuestManager(ICorruption corruption)
        {
            _corruption = corruption ?? throw new ArgumentNullException(nameof(corruption));
            _quests = new Dictionary<string, QuestData>();
        }

       
        /// Starts a new quest with the specified type and ID.
       
        public void StartQuest(CustomProjectRPG.Entities.Enum.Enum.QuestType type, string questId)
        {
            if (string.IsNullOrEmpty(questId)) throw new ArgumentException("Quest ID cannot be null or empty.", nameof(questId));
            if (_quests.ContainsKey(questId)) throw new ArgumentException("Quest already exists.", nameof(questId));

            _quests[questId] = new QuestData
            {
                Type = type,
                IsActive = true,
                Progress = 0,
                RequiredProgress = GetRequiredProgress(type),
                IsComplete = false
            };
            QuestUpdated?.Invoke(questId, false); // Notify quest start
        }

       
        /// Updates the progress of a quest.
       
        public void UpdateQuestProgress(string questId, int amount)
        {
            if (!_quests.ContainsKey(questId) || !_quests[questId].IsActive) return;
            if (amount < 0) throw new ArgumentException("Progress amount must be non-negative.", nameof(amount));

            var quest = _quests[questId];
            quest.Progress = Math.Min(quest.Progress + amount, quest.RequiredProgress);
            quest.IsComplete = quest.Progress >= quest.RequiredProgress;
            QuestUpdated?.Invoke(questId, quest.IsComplete);

            if (quest.IsComplete)
            {
                HandleQuestCompletion(questId);
            }
        }

       
        /// Checks if a quest is complete.
       
        public bool IsQuestComplete(string questId)
        {
            return _quests.ContainsKey(questId) && _quests[questId].IsComplete;
        }

       
        /// Checks the favor result for a favor-based quest.
       
        public Enum.FavorResult CheckFavorResult(string questId)
        {
            if (!_quests.ContainsKey(questId) || _quests[questId].Type != CustomProjectRPG.Entities.Enum.Enum.QuestType.Favor)
            {
                return CustomProjectRPG.Entities.Enum.Enum.FavorResult.Invalid;
            }

            var quest = _quests[questId];
            if (quest.IsComplete)
            {
                return CustomProjectRPG.Entities.Enum.Enum.FavorResult.Success;
            }
            float favorThreshold = 50f + (_corruption.CorruptionPercentage * 0.5f); // Adjust based on corruption
            return quest.Progress >= favorThreshold ? CustomProjectRPG.Entities.Enum.Enum.FavorResult.Success : CustomProjectRPG.Entities.Enum.Enum.FavorResult.Insufficient;
        }

       
        /// Handles logic after quest completion (e.g., rewards, dialogue updates).
       
        private void HandleQuestCompletion(string questId)
        {
            var quest = _quests[questId];
            quest.IsActive = false; // Mark as inactive after completion
                                    // Example: Trigger dialogue update via DialogueManager (assumes integration)
                                    // dialogueManager.ShowDialogue(questId, "completed");
        }

       
        /// Determines the required progress based on quest type.
       
        private int GetRequiredProgress(CustomProjectRPG.Entities.Enum.Enum.QuestType type)
        {
            return type switch
            {
                CustomProjectRPG.Entities.Enum.Enum.QuestType.Daily => 5,
                CustomProjectRPG.Entities.Enum.Enum.QuestType.Story => 10,
                CustomProjectRPG.Entities.Enum.Enum.QuestType.Favor => 8,
                _ => 1
            };
        }

       
        /// Serializes the quest state to JSON.
       
        public string Serialize()
        {
            var data = new QuestManagerData
            {
                Quests = _quests
            };
            return JsonSerializer.Serialize(data);
        }

       
        /// Deserializes the quest state from JSON.
       
        public void Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<QuestManagerData>(json)
                ?? throw new JsonException("Invalid JSON format for quest state.");
            _quests = data.Quests ?? new Dictionary<string, QuestData>();
        }

        private class QuestData
        {
            [JsonPropertyName("type")]
            public CustomProjectRPG.Entities.Enum.Enum.QuestType Type { get; set; }

            [JsonPropertyName("isActive")]
            public bool IsActive { get; set; }

            [JsonPropertyName("progress")]
            public int Progress { get; set; }

            [JsonPropertyName("requiredProgress")]
            public int RequiredProgress { get; set; }

            [JsonPropertyName("isComplete")]
            public bool IsComplete { get; set; }
        }

        private class QuestManagerData
        {
            [JsonPropertyName("quests")]
            public Dictionary<string, QuestData> Quests { get; set; }
        }
    }
}
