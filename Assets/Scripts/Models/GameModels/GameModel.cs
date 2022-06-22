using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Models.GameModels
{
   
    public abstract class GameModel
    {
        private DateTime _gameStartTime;

        public bool GameOver = false;
        
        public GameModel()
        {
            _gameStartTime = DateTime.Now;
            
        }
        
        

        public virtual void MoveToNextTurn()
        {
            
        }
        
        public void GameIsOver()
        {
            GameOver = true;
        }
    }
}
