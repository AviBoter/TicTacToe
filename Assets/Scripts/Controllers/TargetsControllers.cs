using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class TargetsControllers : MonoBehaviour
    {
        private TargetsModel _targetsModel;
        private TargetsView _targetsView;
        private GameButtonsView _gameButtonsView;
      
        void Awake()
        {
            _targetsModel = new TargetsModel();
            _targetsView = FindObjectOfType<TargetsView>();
            _gameButtonsView = FindObjectOfType<GameButtonsView>();
        }
        
        void Start()
        {
            _gameButtonsView.OnUndoButtonPressedAction += OnUndoButtonPressed;
            _gameButtonsView.OnRestartButtonPressedAction += OnRestartButtonPressed;
            _gameButtonsView.OnHintButtonPressedAction += OnHintButtonPressed;

            _targetsModel.OnDeleteLastMoveFromListAction += OnDeleteLastMoveFromModel;
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
    }
}
