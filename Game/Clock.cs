using System.Timers;
using Timer = System.Timers.Timer;

namespace Metamon.Game
{
    public static class Clock
    {
        public static GameTimer CombatTimer { get; set; } = new GameTimer();
    }

    public class GameTimer
    {
        public event EventHandler? OnTick;

        private Timer? _timer;

        public void Start(double intervalMs = 1000)
        {
            if (_timer != null) return; // prevent multiple starts

            _timer = new Timer(intervalMs);
            _timer.Elapsed += Tick;
            _timer.AutoReset = true;
            _timer.Start();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        private void Tick(object? sender, ElapsedEventArgs e)
        {
            OnTick?.Invoke(null, EventArgs.Empty);
        }
    }
}