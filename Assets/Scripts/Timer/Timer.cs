using System;
using StaticClasses;

namespace Timer
{
    public class Timer : BaseTimer
    {
        // PROPERTIES
        private readonly float _delta;
        private float _timeToNextTick;

        // CONSTRUCTORS
        public Timer(float deltaTime, Action<BaseTimer> callback) : this(deltaTime, callback, false, 0)
        {
        }

        public Timer(float deltaTime, Action<BaseTimer> callback, bool repeating, float firstCycleDeltaTime) : this(
            deltaTime, callback, repeating, firstCycleDeltaTime, false)
        {
        }

        public Timer(float deltaTime, Action<BaseTimer> callback, bool repeating, float firstCycleDeltaTime,
            bool ignoreTimescale) : base(callback, repeating)
        {
            _delta = deltaTime;
            _timeToNextTick = firstCycleDeltaTime > 0 ? firstCycleDeltaTime : _delta;
            IgnoresTimeScale = ignoreTimescale;
        }

        // MonoBehaviour Overrides
        public override void Update()
        {
            _timeToNextTick -= IgnoresTimeScale ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime;
           
            while (_timeToNextTick <= 0 && !Finished)
            {
                CallCallback();
                if (Repeating)
                {                   
                    _timeToNextTick += _delta;
                }
                else
                {
                    Finished = true;
                }
            }            
        }
        
        // PUBLIC METHODS
        public void Restart()
        {
            _timeToNextTick = _delta;
            Finished = false;
        }
        public float TimeLeft()
        {
            return _timeToNextTick;
        }
    }
}