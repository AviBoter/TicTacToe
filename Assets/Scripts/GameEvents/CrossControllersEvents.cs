using System;
using Models;
using Models.GameModels;

namespace GameEvents
{
    public class CrossControllersEvents
    {
        public Action<string> OnTournamentStartAction;
        public Action<TournamentDefinition> OnTournamentDefinitionPLayAction;
        
        public Action OnPlayerPressTargetAction;
        
        public Action<GameState> GameOverAction;
    }
}