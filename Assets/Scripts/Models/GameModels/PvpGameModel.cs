using System;
using StaticClasses;
using UnityEngine;

namespace Models.GameModels
{
    public class PvpGameModel : GameModel
    {
        public override event Action<float,bool> OnMoveToNextTurnEventAction;
        public override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (!GameOver)
            {
                OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,_isPlayer1);
            }
        }
    }
}
