using System.Collections.Generic;
using System.Linq;
using GameEvents;
using Models;
using Models.GameModels;
using UnityEngine;
using Views;

namespace Controllers
{
    public class Lookup
    {
        private static Lookup instance;

        public static Lookup Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Lookup();
                }

                return instance;
            }
            set => instance = value;

        }

        private Lookup()
        {
        }

        //Models

        #region
        
        
        public GameModel GameModel { get; set; }
        
        public IGameInitiator GameInitiator;
        
        #endregion

        //Controllers

        #region


        #endregion

        //Views

        #region

        List<ITimerView> _clientTimerView = new List<ITimerView>();

        public List<ITimerView> ClientTimerView
        {
            get
            {
                if (_clientTimerView.Count == 0)
                    _clientTimerView = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ITimerView>()
                        .Where(t => ((TurnTimerView)t)._playerTimer==PlayerTimer.ClientTimer).ToList();
                return _clientTimerView;
            }
            set { _clientTimerView = value; }
        }
        
        private List<ITimerView> _opponentTimerView = new List<ITimerView>();

        public List<ITimerView> OpponentTimerView
        {
            get
            {
                if (_opponentTimerView.Count == 0)
                    _opponentTimerView = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ITimerView>()
                        .Where(t => ((TurnTimerView)t)._playerTimer==PlayerTimer.OpponentTimer).ToList();
                return _opponentTimerView;
            }
            set { _opponentTimerView = value; }
        }
        #endregion

        private CrossControllersEvents _crossControllersEvents;
        public CrossControllersEvents CrossControllersEvents 
        {
            get
            {
                if (_crossControllersEvents == null)
                    _crossControllersEvents = new CrossControllersEvents();
                return _crossControllersEvents;
            }
            set
            {
                _crossControllersEvents = value;
            }
        }
        
        public FadeView FadeView { set; get; }

    }
}

