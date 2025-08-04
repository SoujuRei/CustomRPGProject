using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Entities.Enum

{
    public static class Enum
    {
        public enum BeastType { Weak, Elite, Boss }
        public enum CorruptionState { Normal, Partial, Full }
        public enum FavorResult { Success, Insufficient, Invalid }

        public enum QuestType { Daily, Story, Favor }

        public enum EntityType { Player, NPC, Beast }
    }
}
