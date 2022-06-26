using System;

namespace Models.GameModels
{
    public enum GameState
    {
        OnGoing = 0, XWin = 1, OWin = 2, Tie = 3
    }
    public abstract class GameModel
    {
        protected bool GameOver = false;
        
        [NonSerialized]
        public bool _isPlayer1 = true;
        
        public abstract event Action<float,bool> OnMoveToNextTurnEventAction;

        public virtual void MoveToNextTurn()
        {
            _isPlayer1 = !_isPlayer1;
        }
        
        public void GameIsOver()
        {
            GameOver = true;
        }

    }
}
