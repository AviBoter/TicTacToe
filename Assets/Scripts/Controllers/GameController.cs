using System.Collections;
using System.Collections.Generic;
using GameEvents;
using Models.GameModels;
using StaticClasses;
using Timer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private GameModel _gameModel => Lookup.Instance.GameModel;  
        private CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        private List<ITimerView> _clientTimerView => Lookup.Instance.ClientTimerView;
        private List<ITimerView> _opponentTimeView => Lookup.Instance.OpponentTimerView;

        private void Awake()
        {
            _controllersEvents.OnPlayerPressTargetAction += PlayerPressedTarget;
            _controllersEvents.OnPlayerPressRestartAction += PlayerPressedRestartAndUndo;
            _controllersEvents.OnPlayerPressUndoAction += PlayerPressedRestartAndUndo;
        }

        private void Start()
        {
            StartCoroutine(AssignListeners());
        }
        
        private IEnumerator AssignListeners()
        {
            yield return new WaitUntil(() => Lookup.Instance.CrossControllersEvents != null);
            _controllersEvents.GameOverAction += GameOver;
            yield return new WaitUntil(() => _gameModel != null);
            _gameModel.OnMoveToNextTurnEventAction += OnTimerStarted;
            if (_gameModel is PvCGameModel)
            {
                PvCGameModel model = (PvCGameModel)_gameModel;
                model.OnComputerTurnAction += OnComputerTurn;
            }
            
            yield return new WaitUntil(() => _clientTimerView != null);
            OnTimerStarted(5, true);
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
                turnTimerView.OnTimerIsFinishedAction -= GameOver;
            });
            startTimers.ForEach(timerView =>
            {
                timerView.StartTimerView(time);
                TurnTimerView turnTimerView = (TurnTimerView)timerView;
                turnTimerView.OnTimerIsFinishedAction += GameOver;
            });
        }

        private void OnTimerStopped(float stopTime, bool isPlayer1)
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
            _controllersEvents.OnPlayerPressTargetAction -= PlayerPressedTarget;
            _controllersEvents.GameOverAction -= GameOver;
            _gameModel.OnMoveToNextTurnEventAction -= OnTimerStarted;
            OnTimerStopped(0,_gameModel._isPlayer1);
            ShowMessage(state);
            StartCoroutine(ExitTheGame());
        }

        private void ShowMessage(GameState state)
        {
            if (state == GameState.Tie)
            {
                MessagesManager.Instance.ShowMessage("Draw!",3);
            }
            else
            {
                if (state == GameState.OnGoing)
                {
                    state = _gameModel._isPlayer1 ? GameState.OWin : GameState.XWin;
                }
                MessagesManager.Instance.ShowMessage((state == GameState.XWin ? "Player1" : "Player2") +" Win the Game!",3);
            }
        }

        private IEnumerator ExitTheGame()
        {
            TimerRunner.SharedInstance.FireTimer(2, (time) =>
            {
                Lookup.Instance.FadeView.AnimateFadeIn(1);
            });
            yield return new WaitForSeconds(3);
            Lookup.Instance.GameModel = null;
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

        private void PlayerPressedRestartAndUndo()
        {
            OnTimerStarted(5,true);
            _gameModel._isPlayer1 = false;
            _gameModel.MoveToNextTurn();
        }

        private void OnComputerTurn(PlayerType playerType)
        {
            Lookup.Instance.CrossControllersEvents.OnComputerTurnAction?.Invoke(playerType);
        }
       
        
    }
}