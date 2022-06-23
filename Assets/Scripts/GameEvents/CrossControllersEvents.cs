using System;
using Models;

namespace GameEvents
{
    public class CrossControllersEvents
    {
        public Action<string> OnTournamentStartAction;
        public Action<TournamentDefinition> OnTournamentDefinitionPLayAction;
        public Action GameOverAction;
    }
}
