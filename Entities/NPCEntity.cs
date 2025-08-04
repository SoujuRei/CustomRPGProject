using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CustomProjectRPG.Interfaces;
using Enum = CustomProjectRPG.Entities.Enum.Enum;
using CustomProjectRPG.Core.Utilities;
using CustomProjectRPG.Entities.Component;


namespace CustomProjectRPG.Entities
{
        public class NPCEntity : Entity, IDayListener
    {
        private string _id;
        

        public NPCEntity(string id, string name, Vector2D Position, string description, Vector2D hitboxSize)
            : base(name, description, Position, hitboxSize)
        {
            
        }

        public bool CanTalkTo(PlayerEntity player) => _isAvailable;

        public DialogueNode GetDialogue(PlayerEntity player) => _dialogueState.GetInitialNode(player);

        public void TriggerDialogue(PlayerEntity player)
        {
            _visitCount++;
            // logic to run side effects of dialogue
        }

        public Request? OfferRequest(PlayerEntity player)
        {
            return _requestList.FirstOrDefault(r => !r.IsCompleted());
        }

        // Accept a request (add if not present)
        public void AcceptRequest(Request req)
        {
            if (!_requestList.Contains(req))
                _requestList.Add(req);
            // If you want to track acceptance, you can add a flag in your Request subclasses
        }

        

        public void CompleteQuest(PlayerEntity player)
        {
            var q = _requestList.FirstOrDefault(r => !r.IsCompleted());
            if (q != null)
            {
                q.Complete();
                // If the request is a FavorRequest, you may want to give favor, etc.
                // Example: if (req is FavorRequest favorReq) { player.GiveFavor(this, favorReq.GetRequiredFavor()); }
                // For other types, handle accordingly.
            }
        }

        public bool HasAvailableQuest() => _requestList.Any(r => !r.IsCompleted());

        public bool IsAvailable() => _isAvailable;

        public FavorResult ExecuteCommand(System.Windows.Input.ICommand cmd)
        {
            return cmd.Execute(this, null); // You can pass Player if needed
        }

        public void IncreaseFavor(int amount) => _favorBalance.IncreasePlayerFavor(amount);
        public void ReceiveFavor(int amount) => _favorBalance.IncreaseNPCFavor(amount);
        public void Block() => _isAvailable = false;

        public void OnDayChanged(int day)
        {
            foreach (var q in _requestList) 
            { 
            
            }
               
        }

        public FavorLedger FavorBalance()
        { 
            return _favorBalance;
        }
        public override void Update() { }
        public override void Draw() { }
    }

}