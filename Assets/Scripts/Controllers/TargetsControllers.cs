using System;
using System.Collections.Generic;
using GameEvents;
using Models;
using Models.GameModels;
using UnityEngine;
using Views;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TargetsControllers : MonoBehaviour
    {
        private TargetsModel _targetsModel;
        private TargetsView _targetsView;
        private GameButtonsController _gameButtonsController;

        private GameModel _gameModel => Lookup.Instance.GameModel;
        private CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        void Awake()
        {
            _targetsModel = new TargetsModel();
            _targetsView = FindObjectOfType<TargetsView>();
            _gameButtonsController = FindObjectOfType<GameButtonsController>();
            _targetsView.OnPlayerPressTargetEventAction += OnTargetPressedByPlayer;
        }
        
        void Start()
        {
            _gameButtonsController.OnUndoButtonPressedAction += OnUndoButtonPressed;
            _gameButtonsController.OnRestartButtonPressedAction += OnRestartButtonPressed;
            _gameButtonsController.OnHintButtonPressedAction += OnHintButtonPressed;
            _targetsModel.OnDeleteLastMoveFromListAction += OnDeleteLastMoveFromModel;
            _targetsModel.OnGameStateChanged += GameOver;
            _controllersEvents.OnComputerTurnAction += OnTargetPressedByComputer;
        }

        #region undoRelated

        private void OnUndoButtonPressed()
        {
            _targetsModel.DeleteLastMoveFromList();
        }

        private void OnDeleteLastMoveFromModel(KeyValuePair<int, int> location)
        {
            _targetsView.RemoveTargetAtLocation(location);
        }

        #endregion
      
        #region restartRelated
        
        private void OnRestartButtonPressed()
        {
            _targetsModel.Reset();
            _targetsView.ResetView();
        }
        
        #endregion
        
        #region hintRelated
        
        private void OnHintButtonPressed()
        {
            OnTargetSuggestedToPlayer(PlayerType.X);
        }
        
        #endregion
        
        private void OnTargetPressedByPlayer(KeyValuePair<int,int> location)
        {
            bool result = _targetsModel.PlayerPressTargetButton(location);
            if (result)
            {
                _targetsView.AddTargetAtLocation(location,_gameModel._isPlayer1 ? PlayerType.X : PlayerType.O);
                _controllersEvents.OnPlayerPressTargetAction?.Invoke();
            }
        }
        
        private void OnTargetPressedByComputer(PlayerType type)
        { 
            bool randomTargetFound = false;
           while(!randomTargetFound)
           {
               int xRandom = Random.Range(0, 3);
               int yRandom = Random.Range(0, 3);
               KeyValuePair<int, int> location = new KeyValuePair<int, int>(xRandom, yRandom);
               bool result = _targetsModel.PlayerPressTargetButton(location);
               if (result)
               {
                   randomTargetFound = true;
                   _targetsView.CreateNewTarget(location.Key,location.Value , false);
                   _targetsView.AddTargetAtLocation(location, type);
                   _controllersEvents.OnPlayerPressTargetAction?.Invoke();
               }
           }
        }
        
        private void OnTargetSuggestedToPlayer(PlayerType type)
        { 
            bool randomTargetFound = false;
            while(!randomTargetFound)
            {
                int xRandom = Random.Range(0, 3);
                int yRandom = Random.Range(0, 3);
                KeyValuePair<int, int> location = new KeyValuePair<int, int>(xRandom, yRandom);
                bool result = _targetsModel.IsTargetAvailable(location.Key, location.Value);
                if (result)
                {
                    _targetsView.SuggestTargetAtLocation(location, type);
                    break;
                }
            }
        }

        private void GameOver(GameState gameState)
        {
            _controllersEvents.GameOverAction?.Invoke(gameState);
            _targetsView.OnPlayerPressTargetEventAction -= OnTargetPressedByPlayer;
            _controllersEvents.OnComputerTurnAction -= OnTargetPressedByComputer;
        }

    }
}
