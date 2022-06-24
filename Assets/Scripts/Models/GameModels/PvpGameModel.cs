using System;
using StaticClasses;
using UnityEngine;

namespace Models.GameModels
{
    public class PvpGameModel : GameModel
    {
        public bool Player1Playing { set; get; } = true;

        public override event Action<float,bool> OnMoveToNextTurnEventAction;
        public override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (!GameOver)
            {
                Player1Playing = !Player1Playing;
                OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,Player1Playing);
            }
        }
    }
}
