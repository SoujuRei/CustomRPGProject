using CustomProjectRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomProjectRPG.Entities
{
    public class Item: IIdentifiable, ISerializeJSON<Item>

    {
        private string _id;
        private string _name;
        private string _description; 
        private bool _isUsable;
        
        public Item(string id, string name, string description, bool isUsable)
        {
            _id = id;
            _name = name;
            _description = description; 
            _isUsable = isUsable;
        }

        public string Id { get; }
       
        public string Name { get; }
        public string Description { get; }

        public bool IsUsable { get; }

        public void ApplyEffect(PlayerEntity player)
        {
            // Apply healing, buffs, etc.
        }

        public string Serialize()
        {
            var data = new
            {
                Id = _id,
                Name = _name,
                Description = _description,
                IsUsable = _isUsable
            };
            return JsonSerializer.Serialize(data);
        }

        public void Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ??
                       throw new ArgumentException("Invalid JSON format.", nameof(json));
            _id = data["Id"]?.ToString() ?? throw new ArgumentException("Id is required.", nameof(json));
            _name = data["Name"]?.ToString() ?? throw new ArgumentException("Name is required.", nameof(json));
            _description = data["Description"]?.ToString() ?? throw new ArgumentException("Description is required.", nameof(json));
            _isUsable = data["IsUsable"] is bool usable ? usable : throw new ArgumentException("IsUsable must be a boolean.", nameof(json));
        }
    }

}
