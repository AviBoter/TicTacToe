using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Models.GameModels
{

    public enum GameType
    {
        PvP = 0, PvC = 1, CvC = 2
    }
    public enum GameState
    {
        OnGoing = 0, XWin = 1, OWin = 2, Tie = 3
    }
    public abstract class GameModel
    {
        public bool GameOver = false;
        
        [NonSerialized]
        public bool _isPlayer1 = true;
        
        public abstract event Action<float,bool> OnMoveToNextTurnEventAction;

        private GameState _curGameState { set; get; } = GameState.OnGoing;
        public virtual void MoveToNextTurn()
        {
            _isPlayer1 = !_isPlayer1;
        }
        
        public void GameIsOver()
        {
            GameOver = true;
        }

        public GameState GetGameState()
        {
            return _curGameState;
        }
        
        public void SetGameState(GameState state)
        {
           _curGameState = state;
        }
        
    }
}
