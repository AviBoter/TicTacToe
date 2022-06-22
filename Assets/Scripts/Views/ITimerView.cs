namespace Views
{
    public interface ITimerView
    {
        public void StartTimerView(float time);
        public void StartTimerView(float time, float timeLeft);
        public void StopTimerView(float stopTime);
    }
}