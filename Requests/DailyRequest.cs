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
    public class DailyRequest : Request
    {
        private int _rewardFavor;
        private int _dailyLimit;

        public int RewardFavor => _rewardFavor;
        public int DailyLimit => _dailyLimit;

        public DailyRequest(
            string id,
            string title,
            string description,
            int apCost,
            List<string> requiredFlags,
            List<string> grantedFlags,
            List<string> requiredClues,
            List<string> grantedClues,
            int rewardFavor,
            int dailyLimit,
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
            _requiredFavor = 0;
            _rewardFavor = rewardFavor;
            _dailyLimit = dailyLimit;
            _rewards = rewards;
        }

        public override bool CanOffer(PlayerEntity player)
        {
            // includes daily limit checks, flags, etc.
        }
    }
}
