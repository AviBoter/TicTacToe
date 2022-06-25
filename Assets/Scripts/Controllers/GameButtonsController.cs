using System;
using UnityEngine;

namespace Controllers
{
    public class GameButtonsController : MonoBehaviour
    {
        private bool _isPvP { get; set; } = false;

        public event Action OnUndoButtonPressedAction;
        public event Action OnRestartButtonPressedAction;
        public event Action OnHintButtonPressedAction;
        public void OnUndoButtonPressed()
        {
            if (!_isPvP)
            {
                OnUndoButtonPressedAction?.Invoke();
                Lookup.Instance.CrossControllersEvents.OnPlayerPressUndoAction.Invoke();
            }
        }
    
        public void OnRestartButtonPressed()
        {
            OnRestartButtonPressedAction?.Invoke();
            Lookup.Instance.CrossControllersEvents.OnPlayerPressRestartAction.Invoke();
        }
    
        public void OnHintButtonPressed()
        {
            if (!_isPvP)
            {
                OnHintButtonPressedAction?.Invoke();
            }
        }
    }
}
