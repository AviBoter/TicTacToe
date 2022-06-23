using System.Collections;
using System.Collections.Generic;
using Models.GameModels;
using UnityEngine;
using Views;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private GameModel _gameModel => Lookup.Instance.GameModel;
        List<ITimerView> _clientTimerView => Lookup.Instance.ClientTimerView;
        List<ITimerView> _opponentTimeView => Lookup.Instance.OpponentTimerView;
        
        public void Awake()
        {
            StartCoroutine(AssignListeners());
        }
        
        IEnumerator AssignListeners()
        {
            yield return new WaitUntil(() => Lookup.Instance.CrossControllersEvents != null);
            Lookup.Instance.CrossControllersEvents.GameOverAction += GameOver;
            
            yield return new WaitUntil(() => Lookup.Instance.GameModel != null);
            OnTimerStarted(5, true);

        }
        
        private void OnTimerStarted(float time, bool isClient)
        {
            if (isClient)
            {
                _opponentTimeView.ForEach(timerView => timerView.StopTimerView(0));
                _clientTimerView.ForEach(timerView => timerView.StartTimerView(time));
            }
            else
            {
                _clientTimerView.ForEach(timerView => timerView.StopTimerView(0));
                _opponentTimeView.ForEach(timerView => timerView.StartTimerView(time));
            }
        }
        
        private void OnTimerStopped(float stopTime, bool isClient = true)
        {
            if (isClient)
                _clientTimerView?.ForEach(timerView => timerView.StopTimerView(stopTime));
            else
                _opponentTimeView?.ForEach(timerView => timerView.StopTimerView(stopTime));
        }

        private void GameOver()
        {
            OnTimerStopped(0);
        }
    }
}