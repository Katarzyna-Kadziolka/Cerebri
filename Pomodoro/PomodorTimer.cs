using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;
using Cerebri;

namespace Pomodoro {
    public class PomodoroTimer {
        public PomodoroTimer() {
            CreateDispatcher();
        }

        public event EventHandler OnCountDownEnded;
        public event EventHandler<TickEventArgs> OnTick;

        private DispatcherTimer _timer;
        private TimeSpan _time = TimeSpan.FromMinutes(25);

        public void Start() {
            _timer.Start();
        }

        public void Stop() {
            _timer.Stop();
        }

        public void SetTime(TimeSpan time) {
            _time = time;
        }

        private void CreateDispatcher() {
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, DispatcherTick,
                Application.Current.Dispatcher);
        }

        private void DispatcherTick(object sender, EventArgs e) {
            OnTick?.Invoke(this, new TickEventArgs() { TimeToEnd = _time });
            if (_time == TimeSpan.Zero) {
                Stop();
                OnCountDownEnded?.Invoke(this, EventArgs.Empty);
                return;
            }

            _time = _time.Add(TimeSpan.FromSeconds(-1));
        }
    }
}