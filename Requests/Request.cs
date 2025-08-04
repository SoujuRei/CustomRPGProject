using CustomProjectRPG.Entities;
using CustomProjectRPG.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProjectRPG.Interfaces;

namespace CustomProjectRPG.Requests
{
    

       public abstract class Request : IIdentifiable, ISerializeJSON<Request>
        {
            protected string _id;
            protected string _title;
            protected string _description;
            protected int _apCost;
            protected List<string> _requiredFlags;
            protected List<string> _grantedFlags;
  
            protected int _requiredFavor;
            protected List<Reward> _rewards;

            public string Id { get { return _id; } }
            public string Title { get { return _title; } }
            public string Description { get { return _description; } }
            public int APCost { get { return _apCost; } }
            public IReadOnlyList<string> RequiredFlags { get { return _requiredFlags.AsReadOnly(); } }
            public IReadOnlyList<string> GrantedFlags { get { return _grantedFlags.AsReadOnly(); } }
            
            public int RequiredFavor { get { return _requiredFavor; } }
            public IReadOnlyList<Reward> Rewards { get { return _rewards.AsReadOnly(); } }

            protected Request(
                string id,
                string title,
                string description,
                int apCost,
                List<string> requiredFlags,
                List<string> grantedFlags,
                List<string> requiredClues,
                List<string> grantedClues,
                int requiredFavor,
                List<Reward> rewards)
            {
                _id = id;
                _title = title;
                _description = description;
                _apCost = apCost;
                _requiredFlags = requiredFlags ?? new List<string>();
                _grantedFlags = grantedFlags ?? new List<string>();
                
                _requiredFavor = requiredFavor;
                _rewards = rewards ?? new List<Reward>();
            }

            public virtual bool CanOffer(PlayerEntity player)
            {
                // Check flags, clues, and favor
                foreach (string flag in _requiredFlags)
                {
                    if (!player.Memory.HasFlag(flag)) return false;
                }

                
                return player.Favor.CanUseFavor(player.Id, _requiredFavor);
            }

            public virtual bool CanComplete(PlayerEntity player)
            {
                // Default: always completable if offered
                return true;
            }

            public virtual void ApplyRewards(PlayerEntity player)
            {
                foreach (Reward reward in _rewards)
                {
                    reward.Apply(player);
                }

                foreach (string flag in _grantedFlags)
                {
                    player.Memory.AddFlag(flag);
                }

                foreach (string clue in _grantedClues)
                {
                    player.Memory.AddClue(clue);
                }
            }
        }

    }
}
