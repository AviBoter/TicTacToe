using System;
using System.Collections.Generic;
using System.Linq;

namespace Timer
{
    public class TimerRunner : BaseSharedBehaviour<TimerRunner>
    {
        private readonly List<BaseTimer> _timers = new List<BaseTimer>();

        private void Update()
        {
            foreach (var timer in _timers.ToList())
            {
                timer.Update();
            }

            _timers.RemoveAll(t => t.Finished);
        }

        public global::Timer.Timer FireTimer(float time, Action<BaseTimer> callback, bool repeating = false, float firstCycleTime = 0,
            bool ignoreTimescale = false)
        {
            var newTimer = new global::Timer.Timer(time, callback, repeating, firstCycleTime, ignoreTimescale);
            _timers.Add(newTimer);
            return newTimer;
        }
    }
}