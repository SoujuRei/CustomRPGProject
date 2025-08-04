using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG.Interfaces
{
    public interface IUIManager
    {
        void UpdateCorruptionDisplay(string entityId, Enum.CorruptionState state);
        void UpdateQuestDisplay(string questId, bool isComplete);
        void UpdateDialogueDisplay(string dialogue);
    }
}
