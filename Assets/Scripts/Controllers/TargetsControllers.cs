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
        private GameButtonsController _gameButtonsController;
        private ReplaceGameSkinView _replaceGameSkinView;

        private GameModel _gameModel => Lookup.Instance.GameModel;
        private CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        void Awake()
        {
            _targetsModel = new TargetsModel();
            _targetsView = FindObjectOfType<TargetsView>();
            _gameButtonsController = FindObjectOfType<GameButtonsController>();
            _targetsView.OnPlayerPressTargetEventAction += OnTargetPressedByPlayer;
            HandleReSkin();
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
        
        private void HandleReSkin()
        {
            _replaceGameSkinView = FindObjectOfType<ReplaceGameSkinView>();
            if (_replaceGameSkinView != null)
            {
                Debug.Log("got HandleReSkin");
                _targetsView.SetXandOSprites(_replaceGameSkinView.GetXSprite(), _replaceGameSkinView.GetOSprite());
                Sprite obj = _replaceGameSkinView.GetBgSprite();
                Lookup.Instance.CrossControllersEvents.OnReSkinPressedAction(obj);
            }
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
            KeyValuePair<int,int> result = _targetsModel.FindAvailableTarget();
            if (result.Key!=-1)
            {
                _targetsModel.PlayerPressTargetButton(result);
                _targetsView.CreateNewTarget(result.Key,result.Value , false);
                _targetsView.AddTargetAtLocation(result, type);
                _controllersEvents.OnPlayerPressTargetAction?.Invoke();
            }
        }
        
        private void OnTargetSuggestedToPlayer(PlayerType type)
        {
            KeyValuePair<int,int> result = _targetsModel.FindAvailableTarget();
            if (result.Key!=-1)
            {
                _targetsView.SuggestTargetAtLocation(result, type);
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
