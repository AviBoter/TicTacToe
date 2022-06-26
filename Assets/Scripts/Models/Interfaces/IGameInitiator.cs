namespace Models
{
    public enum TournamentDefinition
    {
        PvP,PvC,CvC
    }
    public interface IGameInitiator
    {
        public void StartGame(string tournamentType);
        
        /*todo - add if time will allows to:
        public void ContinueGame(ActiveTournament activeTournament);
        */
    }
}