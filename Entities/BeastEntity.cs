using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using CustomProjectRPG.Interfaces;
using CustomProjectRPG.Entities.Enum;
using CustomProjectRPG.Entities.Component;
using CustomProjectRPG.Core.Utilities;  

namespace CustomProjectRPG.Entities
{
    
   

public class BeastEntity : ISerializeJSON<BeastEntity>
    {
        private CustomProjectRPG.Entities.Enum.Enum.BeastType _beastType;
        private CorruptionComponent _corruption;
        private StatsComponent _stats;
        private bool _isAggressive;

        public event Action<string, CustomProjectRPG.Entities.Enum.Enum.CorruptionState> CorruptionStateChanged; // Event for state updates

        
        /// Initializes a new BeastEntity with the specified beast type and initial stats.
        
        public BeastEntity(CustomProjectRPG.Entities.Enum.Enum.BeastType beastType)
        {
            _beastType = beastType;
            _corruption = new CorruptionComponent(CustomProjectRPG.Entities.Enum.Enum.EntityType.Beast, GetInitialCorruption(beastType));
            _stats = new StatsComponent(CustomProjectRPG.Entities.Enum.Enum.EntityType.Beast, GetInitialHP(), GetInitialATK(), GetInitialDEF(), GetInitialAgility());
            _isAggressive = false;
            UpdateAggressiveness();
        }

        
        /// Gets the initial corruption percentage based on beast type.
        
        private float GetInitialCorruption(CustomProjectRPG.Entities.Enum.Enum.BeastType beastType)
        {
            return beastType switch
            {
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Weak => 0f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Elite => 50f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Boss => 75f,
                _ => 0f
            };
        }

        
        /// Gets the initial HP based on beast type.
        
        private float GetInitialHP()
        {
            return _beastType switch
            {
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Weak => 50f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Elite => 150f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Boss => 300f,
                _ => 50f
            };
        }

        
        /// Gets the initial ATK based on beast type.
        
        private float GetInitialATK()
        {
            return _beastType switch
            {
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Weak => 5f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Elite => 15f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Boss => 25f,
                _ => 5f
            };
        }

        
        /// Gets the initial DEF based on beast type.
        
        private float GetInitialDEF()
        {
            return _beastType switch
            {
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Weak => 2f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Elite => 8f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Boss => 15f,
                _ => 2f
            };
        }

        
        /// Gets the initial Agility based on beast type.
        
        private float GetInitialAgility()
        {
            return _beastType switch
            {
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Weak => 3f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Elite => 10f,
                CustomProjectRPG.Entities.Enum.Enum.BeastType.Boss => 7f,
                _ => 3f
            };
        }

        
        /// Updates the beast's aggressiveness based on corruption state.
        
        private void UpdateAggressiveness()
        {
            var state = _corruption.GetCorruptionState();
            _isAggressive = state == CustomProjectRPG.Entities.Enum.Enum.CorruptionState.Full;
            CorruptionStateChanged?.Invoke($"Beast_{_beastType}", state); // Notify state change
        }

        
        /// Applies damage to the beast, updating stats and checking death.
        
        public void TakeDamage(float amount)
        {
            _stats.TakeDamage(amount);
            if (_stats.HP <= 0)
            {
                // Handle death logic (e.g., drop items, quest update)
                Console.WriteLine($"Beast {_beastType} defeated!");
            }
        }

        
        /// Increases corruption, triggering state and aggressiveness updates.
        
        public void IncreaseCorruption(float amount)
        {
            _corruption.IncreaseCorruption(amount);
            UpdateAggressiveness();
        }

        
        /// Serializes the beast's state to JSON.
        
        public string Serialize()
        {
            var data = new BeastData
            {
                BeastType = _beastType,
                CorruptionState = _corruption.Serialize(),
                StatsState = _stats.Serialize(),
                IsAggressive = _isAggressive
            };
            return JsonSerializer.Serialize(data);
        }

        
        /// Deserializes the beast's state from JSON.
        
        public void Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<BeastData>(json)
                ?? throw new JsonException("Invalid JSON format for beast state.");
            _beastType = data.BeastType;
            _corruption.Deserialize(data.CorruptionState);
            _stats.Deserialize(data.StatsState);
            _isAggressive = data.IsAggressive;
            UpdateAggressiveness();
        }

        private class BeastData
        {
            [JsonPropertyName("beastType")]
            public CustomProjectRPG.Entities.Enum.Enum.BeastType BeastType { get; set; }

            [JsonPropertyName("corruptionState")]
            public string CorruptionState { get; set; }

            [JsonPropertyName("statsState")]
            public string StatsState { get; set; }

            [JsonPropertyName("isAggressive")]
            public bool IsAggressive { get; set; }
        }
    }
}

