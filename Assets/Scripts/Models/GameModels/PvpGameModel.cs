namespace Models.GameModels
{
    public class PvpGameModel : GameModel
    {
        public bool OPlaying { set; get; } = false;
        public bool XPlaying { set; get; } = true;
        protected override void MoveToNextTurn()
        {
            base.MoveToNextTurn();
            if (!GameOver)
            {
                OPlaying = !OPlaying;
                XPlaying = !XPlaying;
                if (XPlaying)
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }
}
