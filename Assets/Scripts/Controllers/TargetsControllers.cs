using System.Collections.Generic;
using GameEvents;
using Models;
using Models.GameModels;
using UnityEngine;
using Views;

namespace Controllers
{
    public class TargetsControllers : MonoBehaviour
    {
        private TargetsModel _targetsModel;
        private TargetsView _targetsView;
        private GameButtonsView _gameButtonsView;

        private GameModel _gameModel => Lookup.Instance.GameModel;
        private CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        void Awake()
        {
            _targetsModel = new TargetsModel();
            _targetsView = FindObjectOfType<TargetsView>();
            _gameButtonsView = FindObjectOfType<GameButtonsView>();
            _targetsView.OnPlayerPressTargetEventAction += OnTargetPressedByPlayer;
        }
        
        void Start()
        {
            _gameButtonsView.OnUndoButtonPressedAction += OnUndoButtonPressed;
            _gameButtonsView.OnRestartButtonPressedAction += OnRestartButtonPressed;
            _gameButtonsView.OnHintButtonPressedAction += OnHintButtonPressed;
            _targetsModel.OnDeleteLastMoveFromListAction += OnDeleteLastMoveFromModel;
            _targetsModel.OnGameStateChanged += GameOver;
            _controllersEvents.OnComputerTurnAction += OnTargetPressedByComputer;
        }

        #region undoRelated

        private void OnUndoButtonPressed()
        {
            _targetsModel.DeleteLastMoveFromList();
        }
        
        private void OnDeleteLastMoveFromModel(PlayerMove playerMove)
        {
            _targetsView.RemoveTargetAtLocation(playerMove.Location);
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
           
        }
        
        #endregion
        
        private void OnTargetPressedByPlayer(KeyValuePair<int,int> location)
        {
            bool result = _targetsModel.PlayerPressTargetButton(location);
            if (result)
            {
                _targetsView.AddTargetAtLocation(location,_gameModel._isPlayer1 ? PLayerType.X : PLayerType.O);
                _controllersEvents.OnPlayerPressTargetAction?.Invoke();
            }
        }
        
        private void OnTargetPressedByComputer()
        {
            Debug.Log("1");
            bool randomTargetFound = false;
            for (int i = 0; i < 3 && !randomTargetFound; i++)
            {
                for (int j = 0; j < 3 && !randomTargetFound; j++)
                {
                    
                    Debug.Log("1");
                    KeyValuePair<int, int> location = new KeyValuePair<int, int>(i, j);
                    bool result = _targetsModel.PlayerPressTargetButton(location);
                    if (result)
                    {
                        randomTargetFound = true;
                        _targetsView.CreateNewTarget(location.Key,location.Value , false);
                        _targetsView.AddTargetAtLocation(location, PLayerType.O);
                        _controllersEvents.OnPlayerPressTargetAction?.Invoke();
                        
                    }
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
