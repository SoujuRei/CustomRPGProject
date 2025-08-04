using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.Interfaces;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color = SplashKitSDK.Color;
using Vector2D = CustomProjectRPG.Core.Utilities.Vector2D;


namespace CustomProjectRPG.UI
{
    public class StaticAsset : ICollidable, IDraw, ISerializeJSON<StaticAsset>
    {
        private readonly string _name;
        private readonly string _description;
        private readonly Hitbox _hitbox;
        private readonly string _inspectionDialogue;

        public string Name => _name;
        public string Description => _description;
        public Hitbox Hitbox => _hitbox;

        public StaticAsset(string name, string description, Vector2D position, Vector2D size, string inspectionDialogue)
        {
            _name = name;
            _description = description;
            _hitbox = new Hitbox(position, size);
            _inspectionDialogue = inspectionDialogue;
        }

        public void Draw()
        {
            // Placeholder: Draw static asset using SplashKit
            SplashKit.FillRectangle(Color.Gray, _hitbox.Position.X, _hitbox.Position.Y, _hitbox.Size.X, _hitbox.Size.Y);
        }

        public string Inspect()
        {
            // Return dialogue for inspection (triggered by player pressing E)
            return _inspectionDialogue;
        }

        public string Serialize()
        {
            var data = new
            {
                Name = _name,
                Description = _description,
                Hitbox = new { Position = _hitbox.Position, Size = _hitbox.Size },
                InspectionDialogue = _inspectionDialogue
            };
            return JsonSerializer.Serialize(data);
        }

        public void Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ??
                       throw new ArgumentException("Invalid JSON format.", nameof(json));
            Name = data["Name"]?.ToString() ?? throw new ArgumentException("Name is required.", nameof(json));
            _description = data["Description"]?.ToString() ?? throw new ArgumentException("Description is required.", nameof(json));
            var hitboxData = data["Hitbox"] as Dictionary<string, object>;
            _hitbox = new Hitbox(
                new Vector2D(float.Parse(hitboxData?["Position"]?["X"]?.ToString() ?? "0"),
                             float.Parse(hitboxData?["Position"]?["Y"]?.ToString() ?? "0")),
                new Vector2D(float.Parse(hitboxData?["Size"]?["X"]?.ToString() ?? "0"),
                             float.Parse(hitboxData?["Size"]?["Y"]?.ToString() ?? "0"))
            );
            _inspectionDialogue = data["InspectionDialogue"]?.ToString() ?? "";
        }
    }
}
