using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG
{

    

    public class CorruptionComponent : ISerializeJSON<CorruptionComponent>, ICorruption
    {
        private float _corruptionPercentage;
        private int _warningCount;
        private Enum.EntityType _entityType;

        public float CorruptionPercentage => _corruptionPercentage;
        public int WarningCount => _warningCount;

       
        // Initializes a new CorruptionComponent for a specific entity type with an optional initial corruption value.
       
        public CorruptionComponent(Enum.EntityType entityType, float initialCorruption = 0f)
        {
            _entityType = entityType;
            _corruptionPercentage = Math.Clamp(initialCorruption, 0f, 100f);
            if (_entityType == Enum.EntityType.Player)
            {
                _warningCount = 0;
            }
        }

       
        // Increases corruption by the specified amount, capping at 100%.
       
        public void IncreaseCorruption(float amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be non-negative.", nameof(amount));
            _corruptionPercentage = Math.Min(_corruptionPercentage + amount, 100f);
        }

       
        // Decreases corruption by the specified amount, flooring at 0%.
       
        public void DecreaseCorruption(float amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be non-negative.", nameof(amount));
            _corruptionPercentage = Math.Max(_corruptionPercentage - amount, 0f);
        }

       
        // Returns the corruption state (used primarily for beasts).
       
        public Enum.CorruptionState GetCorruptionState()
        {
            if (_corruptionPercentage == 0f) return Enum.CorruptionState.Normal;
            if (_corruptionPercentage == 100f) return Enum.CorruptionState.Full;
            return Enum.CorruptionState.Partial;
        }

       
        // Checks if the player should receive a corruption warning from the daughter.
       
        public bool ShouldShowWarning()
        {
            return _entityType == Enum.EntityType.Player && _corruptionPercentage >= 77f && _warningCount < 3;
        }

       
        // Increments the warning count for the player.
       
        public void IncrementWarningCount()
        {
            if (_entityType == Enum.EntityType.Player)
            {
                _warningCount++;
            }
        }

       
        // Checks if the player's corruption triggers the bad ending.
       
        public bool ShouldTriggerBadEnding()
        {
            return _entityType == Enum.EntityType.Player && _corruptionPercentage >= 77f && _warningCount >= 3;
        }

       
        // Checks if an NPC's dialogue is corrupted (≥50% corruption).
       
        public bool IsDialogueCorrupted()
        {
            return _entityType == Enum.EntityType.NPC && _corruptionPercentage >= 50f;
        }

       
        // Checks if an NPC's house is a placeholder (≥60% corruption).
       
        public bool IsHousePlaceholder()
        {
            return _entityType == Enum.EntityType.NPC && _corruptionPercentage >= 60f;
        }

       
        // Checks if an NPC stops responding (≥80% corruption).
       
        public bool IsNoResponse()
        {
            return _entityType == Enum.EntityType.NPC && _corruptionPercentage >= 80f;
        }

       
        // Updates corruption at the start of a new loop based on entity type.
       
        public void OnNewLoop()
        {
            if (_entityType == Enum.EntityType.Player)
            {
                IncreaseCorruption(10f); // Player: +10% per loop
            }
            else if (_entityType == Enum.EntityType.NPC)
            {
                Random rand = new Random();
                float increase = rand.Next(11, 18); // NPC: +11% to 17% per loop
                IncreaseCorruption(increase);
            }
            // Beasts: No change (set at spawn)
        }

       
        // Calculates the stat multiplier for the player based on corruption level.
       
        public float GetStatMultiplier()
        {
            if (_entityType != Enum.EntityType.Player) return 1.0f;
            if (_corruptionPercentage < 50f) return 1.0f;
            int steps = (int)((_corruptionPercentage - 50f) / 10f);
            float multiplier = 0.8f - steps * 0.1f; // 20% decrease at 50%, then 10% per 10%
            return Math.Max(multiplier, 0f);
        }


        public event Action<float> CorruptionChanged;
        private void OnCorruptionChanged() => CorruptionChanged?.Invoke(_corruptionPercentage);

        // Serializes the corruption state to JSON.

        public string Serialize()
        {
            var data = new CorruptionData
            {
                CorruptionPercentage = _corruptionPercentage,
                WarningCount = _warningCount
            };
            return SerializeJSON.ToJson(data);
        }

       
        // Deserializes the corruption state from JSON.
       
        public void Deserialize(string json)
        {
            var data = SerializeJSON.FromJson<CorruptionData>(json);
            _corruptionPercentage = Math.Clamp(data.CorruptionPercentage, 0f, 100f);
            if (_entityType == Enum.EntityType.Player)
            {
                _warningCount = data.WarningCount;
            }
        }

        private class CorruptionData
        {
            [JsonPropertyName("corruptionPercentage")]
            public float CorruptionPercentage { get; set; }

            [JsonPropertyName("warningCount")]
            public int WarningCount { get; set; }
        }

    }
}
