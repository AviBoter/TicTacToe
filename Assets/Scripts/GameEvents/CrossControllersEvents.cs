using System;
using Models;
using Models.GameModels;
using Models.Interfaces;
using UnityEngine;

namespace GameEvents
{
    public class CrossControllersEvents
    {
        public Action<string> OnTournamentStartAction;
        
        public Action<TournamentDefinition> OnTournamentDefinitionPLayAction;
        
        public Action OnPlayerPressTargetAction;
        
        public Action<GameState> GameOverAction;
        
        public Action<int, int, bool> OnPlayerPressTarget;
        
        public Action<PlayerType> OnComputerTurnAction;
        
        public Action OnPlayerPressRestartAction;
        
        public Action OnPlayerPressUndoAction;
        
        public Action<Sprite> OnReSkinPressedAction;
    }
}
