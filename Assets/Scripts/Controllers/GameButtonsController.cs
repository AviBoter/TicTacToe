using System;
using UnityEngine;

namespace Controllers
{
    public class GameButtonsController : MonoBehaviour
    {
        public event Action OnUndoButtonPressedAction;
        public event Action OnRestartButtonPressedAction;
        public event Action OnHintButtonPressedAction;
        public void OnUndoButtonPressed()
        {
            OnUndoButtonPressedAction?.Invoke();
            Lookup.Instance.CrossControllersEvents.OnPlayerPressUndoAction.Invoke();
        }
    
        public void OnRestartButtonPressed()
        {
            OnRestartButtonPressedAction?.Invoke();
            Lookup.Instance.CrossControllersEvents.OnPlayerPressRestartAction.Invoke();
        }
    
        public void OnHintButtonPressed()
        {
            OnHintButtonPressedAction?.Invoke();
        }
    }
}
