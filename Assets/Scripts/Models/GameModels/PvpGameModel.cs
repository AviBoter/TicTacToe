using System;
using StaticClasses;
using UnityEngine;

namespace Models.GameModels
{
    public class PvpGameModel : GameModel
    {
        public bool OPlaying { set; get; } = false;
        public bool XPlaying { set; get; } = true;

        public override event Action<float,bool> OnMoveToNextTurnEventAction;
        public override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (!GameOver)
            {
                OPlaying = !OPlaying;
                XPlaying = !XPlaying;
                if (XPlaying)
                {
                    OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,true);
                }
                else
                {
                    OnMoveToNextTurnEventAction?.Invoke(GlobalValues.TurnTime,false);
                }
            }
        }
    }
}
