using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Pattern: State Pattern 



namespace CustomProjectRPG.GameState
{
    interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
    }

   

    class GameStateManager
    {
        private IGameState currentState;
        public void ChangeState(IGameState newState) { currentState.Exit(); currentState = newState; currentState.Enter(); }
    }
}