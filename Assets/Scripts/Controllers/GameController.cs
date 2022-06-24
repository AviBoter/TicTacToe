using System.Collections;
using System.Collections.Generic;
using Models.GameModels;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            
            yield return new WaitUntil(() => _clientTimerView != null);
            OnTimerStarted(5, true);
            yield return new WaitUntil(() => _gameModel != null);
            _gameModel.OnMoveToNextTurnEventAction += OnTimerStarted;
            Lookup.Instance.CrossControllersEvents.OnPlayerPressTargetAction += PlayerPressedTarget;

        }
        
        private void OnTimerStarted(float time, bool isPlayer1)
        {
            if (isPlayer1)
            {
                AssignTimerViewActionsListeners(time,_opponentTimeView,_clientTimerView);
            }
            else
            {
                AssignTimerViewActionsListeners(time,_clientTimerView,_opponentTimeView);
            }
        }
        
        private void AssignTimerViewActionsListeners(float time, List<ITimerView> stopTimers, List<ITimerView> startTimers)
        {
            stopTimers.ForEach(timerView =>
            {
                timerView.StopTimerView(0);
                TurnTimerView turnTimerView = (TurnTimerView)timerView;
                turnTimerView.OnTimerIsFinishedAction -= NextTurn;
            });
            startTimers.ForEach(timerView =>
            {
                timerView.StartTimerView(time);
                TurnTimerView turnTimerView = (TurnTimerView)timerView;
                turnTimerView.OnTimerIsFinishedAction += NextTurn;
            });
        }

        private void OnTimerStopped(float stopTime, bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                _clientTimerView?.ForEach(timerView => timerView.StopTimerView(stopTime));
            }
            else
            {
                _opponentTimeView?.ForEach(timerView => timerView.StopTimerView(stopTime));
            }
        }

        private void GameOver(GameState state)
        {
            Lookup.Instance.CrossControllersEvents.OnPlayerPressTargetAction -= PlayerPressedTarget;
            Lookup.Instance.CrossControllersEvents.GameOverAction -= GameOver;
            OnTimerStopped(0);
            _gameModel.SetGameState(state);
            if (state == GameState.Tie)
            {
                MessagesManager.Instance.ShowMessage("Game Over!",3);
            }
            else
            {
                MessagesManager.Instance.ShowMessage((state == GameState.XWin ? "Player1" : "Player2") +" Win the Game!",3);
            }

            StartCoroutine(ExitTheGame());
        }

        private IEnumerator ExitTheGame()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0);
        }

        private void NextTurn()
        {
            _gameModel.MoveToNextTurn();
        }

        private void PlayerPressedTarget()
        {
            OnTimerStopped(0, _gameModel._isPlayer1);
            NextTurn();
        }
    }
}