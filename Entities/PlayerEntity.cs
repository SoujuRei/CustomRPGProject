using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProjectRPG.Dialogue;
using CustomProjectRPG.Entities;
using CustomProjectRPG.Entities.Component;
using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.Interfaces;
using CustomProjectRPG.Manager;
using CustomProjectRPG.Tracker;
using Vector2D = CustomProjectRPG.Core.Utilities.Vector2D;

using SplashKitSDK;

namespace CustomProjectRPG.Entities
{
    // Entities/Player.cs

    public class PlayerEntity : Entity
    {
        public StatsComponent Stats { get; }
        public InventoryComponent Inventory { get; }
        public APComponent AP { get; private set; }
        public CorruptionComponent Corruption { get; private set; }
        public FavorComponent Favor { get; private set; }
        public DialogueComponent Dialogue { get; private set; }
        public LoopMemory Memory { get; private set; }

        public PlayerEntity(
            string name,
            string description,
            StatsComponent stats,
            InventoryComponent inventory,
            FavorComponent favor,
            APComponent ap,
            CorruptionManager corruption,
            DialogueComponent dialogue,
            LoopMemory memory,
            Vector2D startPosition,
            Vector2D hitboxSize
        ) : base(name, description, startPosition, hitboxSize)
        {
            Stats = stats;
            Inventory = inventory;
            Favor = favor;
            AP = ap;
            Corruption = corruption;
            Dialogue = dialogue;
            Memory = memory;
        }

        public bool UseActionPoint(int amount)
        {
            return AP.UseAP(amount);
        }

        public void ResetForNewLoop()
        {
            AP.Reset();
            Memory.ResetTransient();
            // Corruption persists across loops, so no reset here
        }

        public override void Update()
        {
            // Handle input, movement, interactions, etc.
        }

        public override void Draw()
        {
            // Render player sprite
        }

        public override void OnCollision(ICollidable other)
        {
            // Handle collision logic
        }
    }

}
