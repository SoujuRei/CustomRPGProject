using CustomProjectRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;
using System.Text.Json.Serialization;
using CustomProjectRPG.Core.Utilities;


namespace CustomProjectRPG.Entities.Component
{
   

    public class StatsComponent : ISerializeJSON<StatsComponent>
    {
        private float _hp;
        private float _maxHp;
        private float _atk;
        private float _def;
        private float _agility;
        private Enum.Enum.EntityType _entityType;

        public float HP => _hp;
        public float MaxHP => _maxHp;
        public float ATK => _atk;
        public float DEF => _def;
        public float Agility => _agility;

       
        /// Initializes a new StatsComponent with base stats for a specific entity type.
       
        public StatsComponent(Enum.Enum.EntityType entityType, float initialHp = 100f, float initialAtk = 10f, float initialDef = 5f, float initialAgility = 5f)
        {
            _entityType = entityType;
            _maxHp = initialHp;
            _hp = _maxHp;
            _atk = initialAtk;
            _def = initialDef;
            _agility = initialAgility;
        }

       
        /// Applies damage to the entity's HP, ensuring it doesn't go below 0.
       
        public void TakeDamage(float amount)
        {
            if (amount < 0) throw new ArgumentException("Damage must be non-negative.", nameof(amount));
            _hp = Math.Max(_hp - amount, 0f);
        }

       
        /// Heals the entity, capping HP at MaxHP.
       
        public void Heal(float amount)
        {
            if (amount < 0) throw new ArgumentException("Heal amount must be non-negative.", nameof(amount));
            _hp = Math.Min(_hp + amount, _maxHp);
        }


        /// Applies corruption-based stat penalties using the provided CorruptionComponent.

        public void ApplyCorruptionPenalty(ICorruption corruption)
        {
            if (corruption == null) throw new ArgumentNullException(nameof(corruption));
            if (_entityType != Enum.Enum.EntityType.Player) return;
            float multiplier = corruption.GetStatMultiplier();
            _atk *= multiplier;
            _def *= multiplier;
            _agility *= multiplier;
        }


        /// Resets stats to their base values (e.g., after a loop reset).

        public void ResetStats()
        {
            _hp = _maxHp;
            _atk = 10f; 
            _def = 6f;
            _agility = 5f;
        }

       
        /// Serializes the stats state to JSON.
       
        public string Serialize()
        {
            var data = new StatsData
            {
                HP = _hp,
                MaxHP = _maxHp,
                ATK = _atk,
                DEF = _def,
                Agility = _agility
            };
            return SerializeJSON.ToJson(data);
        }

       
        /// Deserializes the stats state from JSON.
       
        public void Deserialize(string json)
        {
            var data = SerializeJSON.FromJson<StatsData>(json);
            _hp = Math.Clamp(data.HP, 0f, data.MaxHP);
            _maxHp = data.MaxHP;
            _atk = data.ATK;
            _def = data.DEF;
            _agility = data.Agility;
        }

        private class StatsData
        {
            [JsonPropertyName("hp")]
            public float HP { get; set; }

            [JsonPropertyName("maxHp")]
            public float MaxHP { get; set; }

            [JsonPropertyName("atk")]
            public float ATK { get; set; }

            [JsonPropertyName("def")]
            public float DEF { get; set; }

            [JsonPropertyName("agility")]
            public float Agility { get; set; }
        }
    }

}
