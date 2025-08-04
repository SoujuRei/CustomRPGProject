using CustomProjectRPG.Entities;
using CustomProjectRPG.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Requests
{
    public class StoryRequest : Request
    {
        private Item _rewardItem;
        private string _questFlag;

        public string QuestFlag => _questFlag;
        public Item RewardItem => _rewardItem;

        public StoryRequest(
            string id,
            string title,
            string description,
            int apCost,
            List<string> requiredFlags,
            List<string> grantedFlags,
            List<string> requiredClues,
            List<string> grantedClues,
            Item rewardItem,
            string questFlag,
            List<Reward> rewards
        )
        {
            _id = id;
            _title = title;
            _description = description;
            _apCost = apCost;
            _requiredFlags = requiredFlags;
            _grantedFlags = grantedFlags;
            _requiredClues = requiredClues;
            _grantedClues = grantedClues;
            _rewardItem = rewardItem;
            _questFlag = questFlag;
            _requiredFavor = 0;
            _rewards = rewards;
        }
    }
}
