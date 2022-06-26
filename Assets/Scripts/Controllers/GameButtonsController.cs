using System;
using GameEvents;
using UnityEngine;

namespace Controllers
{
    public class GameButtonsController : MonoBehaviour
    {
        private CrossControllersEvents _controllersEvents=> Lookup.Instance.CrossControllersEvents;
        public event Action OnUndoButtonPressedAction;
        public event Action OnRestartButtonPressedAction;
        public event Action OnHintButtonPressedAction;
        public void OnUndoButtonPressed()
        {
            OnUndoButtonPressedAction?.Invoke();
            _controllersEvents.OnPlayerPressUndoAction.Invoke();
        }
    
        public void OnRestartButtonPressed()
        {
            OnRestartButtonPressedAction?.Invoke();
            _controllersEvents.OnPlayerPressRestartAction.Invoke();
        }
    
        public void OnHintButtonPressed()
        {
            OnHintButtonPressedAction?.Invoke();
        }
    }
}
