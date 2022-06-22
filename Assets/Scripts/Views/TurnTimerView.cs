using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using StaticClasses;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(Image))]
    public class TurnTimerView : MonoBehaviour, ITimerView
    {
        Tween tween;
        [SerializeField] public PlayerTimer _playerTimer;
        private Image _image;
        private List<ITimerView> _clientTimerViews => Lookup.Instance.ClientTimerView;
        private List<ITimerView> _opponentTimerViews => Lookup.Instance.OpponentTimerView;
        
        
        float _fillAmount
        {
            set { _image.fillAmount = value; }
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            if (_playerTimer == PlayerTimer.ClientTimer)
            {
                if (!Lookup.Instance.ClientTimerView.Contains(this))
                {
                    Lookup.Instance.ClientTimerView.Add(this);
                }
            }
            else
            {
                if (!Lookup.Instance.OpponentTimerView.Contains(this))
                {
                    Lookup.Instance.OpponentTimerView.Add(this);
                }
            }
        }

        public void StartTimerView(float time)
        {
            _fillAmount = time/ GlobalValues.TurnTime;
            tween?.Kill();
            tween = GetComponent<Image>().DOFillAmount(0, time);
        }

        public void StartTimerView(float time, float timeLeft)
        {
            _fillAmount = timeLeft / time;
            tween.Kill();
            tween = GetComponent<Image>().DOFillAmount(0, timeLeft);
        }

        public void StopTimerView(float stopTime)
        {
            tween?.Kill();
            _fillAmount = 0;
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

    public enum PlayerTimer
    {
        ClientTimer = 0,
        OpponentTimer = 1
    }
}