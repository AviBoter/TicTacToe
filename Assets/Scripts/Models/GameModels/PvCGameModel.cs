using System;
using Controllers;
using StaticClasses;

namespace Models.GameModels
{
    public enum DifficultLevel
    {
        Easy = 1, Medium = 2, Hard = 3
    }
    public class PvCGameModel : GameModel
    {
        public bool ClientPlaying { set; get; } = false;

        public override event Action<float,bool> OnMoveToNextTurnEventAction;
        
        public override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (!GameOver)
            {
                ClientPlaying = !ClientPlaying;
                OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,ClientPlaying);

                if (!ClientPlaying)
                {
                    Lookup.Instance.CrossControllersEvents.OnComputerTurnAction?.Invoke();
                }
             
            }
        }
    }
}
