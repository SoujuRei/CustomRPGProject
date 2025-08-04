using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG.Interfaces
{

    public interface IQuestManager
    {
        void StartQuest(CustomProjectRPG.Entities.Enum.Enum.QuestType type, string questId);
        bool IsQuestComplete(string questId);
        Enum.FavorResult CheckFavorResult(string questId);
    }
}
