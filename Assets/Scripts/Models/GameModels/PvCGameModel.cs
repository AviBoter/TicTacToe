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
        
        public override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (!GameOver)
            {
                OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,_isPlayer1);

                if (!_isPlayer1)
                {
                    //Delay the computer target choose by 1 sec to make the game flow looks good.
                    TimerRunner.SharedInstance.FireTimer(1,
                        (timer) => { Lookup.Instance.CrossControllersEvents.OnComputerTurnAction?.Invoke(PlayerType.O);});
                }
             
            }
        }
    }
}
