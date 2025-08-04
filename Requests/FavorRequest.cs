using CustomProjectRPG.Entities;
using CustomProjectRPG.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Requests
{
    public class FavorRequest : Request
    {
        private int _favorCost;

        public int FavorCost => _favorCost;

        public FavorRequest(
            string id,
            string title,
            string description,
            int apCost,
            List<string> requiredFlags,
            List<string> grantedFlags,
            List<string> requiredClues,
            List<string> grantedClues,
            int favorCost,
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
            _favorCost = favorCost;
            _requiredFavor = favorCost; // internal consistency
            _rewards = rewards;
        }

        public override bool CanOffer(PlayerEntity player)
        {
            // includes favor check from LoopMemory
        }
    }

}
