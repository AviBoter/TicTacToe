
namespace Models.GameModels
{
    public class PvCGameModel : GameModel
    {
        public bool ClientPlaying { set; get; } = false;
        protected override void MoveToNextTurn()
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
