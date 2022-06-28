using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GameEvents;
using Models.GameModels;

namespace Views
{
    public class TargetsView : MonoBehaviour
    {
        CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
            
        [SerializeField] private Sprite _x;  
        [SerializeField] private Sprite _o;
        
        [SerializeField]
        private Image _zeroXZero;
        [SerializeField]
        private Image _zeroXOne;
        [SerializeField]
        private Image _zeroXTwo;
    
        [SerializeField]
        private Image _oneXZero;
        [SerializeField]
        private Image _oneXOne;
        [SerializeField]
        private Image _oneXTwo;
    
        [SerializeField]
        private Image _twoXZero;
        [SerializeField]
        private Image _twoXOne;
        [SerializeField]
        private Image _twoXTwo;
        
        private List<TargetView> _targets;

        public event Action<KeyValuePair<int, int>> OnPlayerPressTargetEventAction;
        private void Awake()
        {
            _targets = GetComponentsInChildren<TargetView>().ToList();
            _controllersEvents.OnPlayerPressTarget += CreateNewTarget;
            _controllersEvents.GameOverAction += GameOver;
            _controllersEvents.OnPlayerPressRestartAction += ResetAllViews;
        }
        
        public void AddTargetAtLocation(KeyValuePair<int,int> location,PlayerType type)
        {
            Image target = GetTargetImage(location);
            
            if (type == PlayerType.O)
            {
                target.sprite = _o;
                target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 1f), 0.5f);
            }
            else
            {
                target.sprite = _x;
                target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 1f), 0.5f);
            }
        }
        
        public void SuggestTargetAtLocation(KeyValuePair<int,int> location,PlayerType type)
        {
            Image target = GetTargetImage(location);
            
            if (type == PlayerType.O)
            {
                target.sprite = _o;
                target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 1f), 0.5f).OnComplete(() =>
                {
                    target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 0f), 0.5f);
                });
            }
            else
            {
                target.sprite = _x;
                target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 1f), 0.5f).OnComplete(() =>
                {
                    target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 0f), 0.5f);
                });
            }
        }

        public void CreateNewTarget(int x, int y , bool player1)
        {
            KeyValuePair<int, int> location = new KeyValuePair<int, int>(x, y);
            OnPlayerPressTargetEventAction?.Invoke(location);
        }
        private Image GetTargetImage(KeyValuePair<int, int> keyValuePair)
        {
            switch (keyValuePair)
            {
                case (0,0):
                    return _zeroXZero;
                case (0,1):
                    return _zeroXOne;
                case (0,2):
                    return _zeroXTwo;
                case (1,0):
                    return _oneXZero;
                case (1,1):
                    return _oneXOne;
                case (1,2):
                    return _oneXTwo;
                case (2,0):
                    return _twoXZero;
                case (2,1):
                    return _twoXOne;
                case (2,2):
                    return _twoXTwo;
            }
            return null;
        }
        
        public void RemoveTargetAtLocation(KeyValuePair<int,int> location)
        {
            Image target = GetTargetImage(location);
            
            target.DOColor(new Color(target.color.r, target.color.g, target.color.b, 0f), 0.5f)
                .OnComplete(()=>
                {
                    target.GetComponent<TargetView>().TargetPressed(false);
                    target.sprite = null;
                });
        }
        
        public void ResetView()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    RemoveTargetAtLocation(new KeyValuePair<int, int>(i, j));
                }
            }
            
            GameOver(GameState.OWin);
        }

        private void GameOver(GameState state)
        {
            _controllersEvents.GameOverAction -= GameOver;
            ResetAllViews();
        }

        private void ResetAllViews()
        {
            if (_targets != null)
            {
                foreach (TargetView view in _targets)
                {
                    view.TargetPressed(false);
                }
            }
            _controllersEvents.OnPlayerPressRestartAction -= ResetAllViews;
        }

        public void SetXandOSprites(Sprite x , Sprite o)
        {
            _x = x;
            _o = o;
        }
    }
}
