namespace Models
{
    public enum TournamentDefinition
    {
        PvP,PvC,CvC
    }
    public interface IGameInitiator
    {
        public void StartGame(TournamentDefinition tournamentDefinition);
        
        /*to be added if time will allows to:
        public void ContinueGame(ActiveTournament activeTournament);
        */
    }
}