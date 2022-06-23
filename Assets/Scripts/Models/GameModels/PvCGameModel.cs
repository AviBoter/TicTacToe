
using System;

namespace Models.GameModels
{
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
                if (ClientPlaying)
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }
}
