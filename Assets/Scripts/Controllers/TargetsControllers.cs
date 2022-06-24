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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    KeyValuePair<int, int> location = new KeyValuePair<int, int>(i, j);
                    bool result = _targetsModel.PlayerPressTargetButton(location);
                    if (result)
                    {
                        _targetsView.AddTargetAtLocation(location,_gameModel._isPlayer1 ? PLayerType.X : PLayerType.O);
                        _controllersEvents.OnPlayerPressTargetAction?.Invoke();
                        break;
                    }
                }
            }
            _gameModel.MoveToNextTurn();
        }

        private void GameOver(GameState gameState)
        {
            _controllersEvents.GameOverAction?.Invoke(gameState);
            _targetsView.OnPlayerPressTargetEventAction -= OnTargetPressedByPlayer;
            _controllersEvents.OnComputerTurnAction -= OnTargetPressedByComputer;
        }

    }
}
