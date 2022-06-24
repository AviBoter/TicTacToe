using System;
using Controllers;
using UnityEngine;

namespace Views
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
            }
        }
    
        public void OnRestartButtonPressed()
        {
            if (!_isPvP)
            {
                OnRestartButtonPressedAction?.Invoke();
                Lookup.Instance.CrossControllersEvents.OnPlayerPressRestartAction.Invoke();
            }
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
