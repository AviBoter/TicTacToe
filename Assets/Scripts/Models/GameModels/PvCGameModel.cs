using System;
using Controllers;
using StaticClasses;
using Timer;

namespace Models.GameModels
{
    public enum DifficultLevel
    {
        Easy = 1, Medium = 2, Hard = 3
    }
    public class PvCGameModel : GameModel
    {
        public override event Action<float,bool> OnMoveToNextTurnEventAction;
        public event Action<PlayerType> OnComputerTurnAction;
        
        public override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (GameOver) return;
            OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,_isPlayer1);
            if (!_isPlayer1)
            {
                //Computer target choose delayed by 1 sec to make the game flow looks smooth.
                TimerRunner.SharedInstance.FireTimer(1, (timer) 
                    => {OnComputerTurnAction?.Invoke(PlayerType.O);});
            }
        }
    }
}
