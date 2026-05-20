using System;
using System.Windows.Threading;

namespace kursach_wpf.framework
{
    public class GameClock
    {
        private readonly DispatcherTimer whiteTimer;
        private readonly DispatcherTimer blackTimer;
        private TimeSpan whiteTimeRemaining;
        private TimeSpan blackTimeRemaining;

        public bool IsWhiteTurn { get; private set; } = true;

        public event Action<TimeSpan, TimeSpan, bool> TimeUpdated;
        public event Action<bool> TimeExpired;

        public GameClock()
        {
            whiteTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            blackTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            whiteTimer.Tick += WhiteTimerTick;
            blackTimer.Tick += BlackTimerTick;
        }

        public void Start(TimeSpan initialTime)
        {
            whiteTimeRemaining = initialTime;
            blackTimeRemaining = initialTime;
            IsWhiteTurn = true;

            NotifyTimeUpdated();
            whiteTimer.Start();
            blackTimer.Stop();
        }

        public void SwitchTurns()
        {
            if (IsWhiteTurn)
            {
                whiteTimer.Stop();
                blackTimer.Start();
            }
            else
            {
                blackTimer.Stop();
                whiteTimer.Start();
            }

            IsWhiteTurn = !IsWhiteTurn;
            NotifyTimeUpdated();
        }

        private void WhiteTimerTick(object sender, EventArgs e)
        {
            if (whiteTimeRemaining.TotalSeconds > 0)
            {
                whiteTimeRemaining = whiteTimeRemaining.Subtract(TimeSpan.FromSeconds(1));
                NotifyTimeUpdated();
                return;
            }

            Stop();
            TimeExpired?.Invoke(true);
        }

        private void BlackTimerTick(object sender, EventArgs e)
        {
            if (blackTimeRemaining.TotalSeconds > 0)
            {
                blackTimeRemaining = blackTimeRemaining.Subtract(TimeSpan.FromSeconds(1));
                NotifyTimeUpdated();
                return;
            }

            Stop();
            TimeExpired?.Invoke(false);
        }

        private void NotifyTimeUpdated()
        {
            TimeUpdated?.Invoke(whiteTimeRemaining, blackTimeRemaining, IsWhiteTurn);
        }

        private void Stop()
        {
            whiteTimer.Stop();
            blackTimer.Stop();
        }
    }
}
