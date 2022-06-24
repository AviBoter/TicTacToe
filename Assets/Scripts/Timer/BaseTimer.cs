using System;

namespace Timer
{
    public abstract class BaseTimer
    {
        public bool Finished { get; protected set; }
        public bool Repeating { get; private set; }
        public bool IgnoresTimeScale { get; set; }

        private Action<BaseTimer> _callback;

        protected BaseTimer(Action<BaseTimer> callback, bool repeating)
        {
            _callback = callback;
            Repeating = repeating;
        }

        public abstract void Update();

        public void Stop()
        {
            Finished = true;
        }

        protected void CallCallback()
        {
            _callback(this);
        }
    }
}