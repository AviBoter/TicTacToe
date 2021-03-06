using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Models.GameModels;
using StaticClasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public enum PlayerTimer
    {
        ClientTimer = 0,
        OpponentTimer = 1
    }
    
    [RequireComponent(typeof(Image))]
    public class TurnTimerView : MonoBehaviour, ITimerView
    {
        Tween tween;
        [SerializeField] public PlayerTimer _playerTimer;
        [SerializeField] public TMP_Text _time;
        private Image _image;
        private List<ITimerView> _clientTimerViews => Lookup.Instance.ClientTimerView;
        private List<ITimerView> _opponentTimerViews => Lookup.Instance.OpponentTimerView;

        private float _turnTime = GlobalValues.TurnTime;
        private bool _countTimeBool = false;
        float _fillAmount
        {
            set { _image.fillAmount = value; }
        }
        
        float _timeCounter
        {
            set {
                if (Mathf.FloorToInt(value) >= 0)
                {
                    _time.text = Mathf.FloorToInt(value).ToString();
                }
            }
                 
        }

        public event Action<GameState> OnTimerIsFinishedAction;

        private void Awake()
        {
            _countTimeBool = false;
            _image = GetComponent<Image>();
            if (_playerTimer == PlayerTimer.ClientTimer)
            {
                if (!_clientTimerViews.Contains(this))
                {
                    _clientTimerViews.Add(this);
                }
            }
            else
            {
                if (!_opponentTimerViews.Contains(this))
                {
                    _opponentTimerViews.Add(this);
                }
            }
        }

        private void Update()
        {
            if (_turnTime > 0 && _countTimeBool)
            {
                _turnTime -= Time.deltaTime;
                _timeCounter = _turnTime;
            }

            else if (_turnTime <= 0 && _countTimeBool)
            {
                _countTimeBool = false;
                _turnTime = 5;
                OnTimerIsFinishedAction?.Invoke(GameState.OnGoing);
            }
        }

        public void StartTimerView(float time)
        {
            _turnTime = 5;
            _timeCounter = 5;
            _fillAmount = 5;
            _fillAmount = time/ GlobalValues.TurnTime;
            _countTimeBool = true;
            tween?.Kill();
            tween = GetComponent<Image>().DOFillAmount(0, time);
        }
        
        public void StartTimerView(float time, float timeLeft)
        {
            _fillAmount = timeLeft / time;
            _timeCounter = timeLeft/time;
            tween.Kill();
            tween = GetComponent<Image>().DOFillAmount(0, timeLeft);
        }

        public void StopTimerView(float stopTime)
        {
            tween?.Kill();
            _countTimeBool = false;
            _turnTime = 5;
            _timeCounter = 5;
            _fillAmount = 5;
        }

        private void OnDestroy()
        {
            if (_clientTimerViews.Contains(this))
            {
                _clientTimerViews.Remove(this);
            }
            if (_opponentTimerViews.Contains(this))
            {
                _opponentTimerViews.Remove(this);
            }
        }
    }
}