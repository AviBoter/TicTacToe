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
        
        protected virtual void MoveToNextTurn()
        {
            
        }
        
        public void GameIsOver()
        {
            GameOver = true;
        }

        public GameState TableState()
        {
            return GameState.OnGoing;
        }
    }
}
