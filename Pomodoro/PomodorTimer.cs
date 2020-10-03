using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Pomodoro {
    public class PomodoroTimer {
        public PomodoroTimer() {
            
        }

        public delegate void OnCountDownEndedEventHandler(object source, EventArgs args);
        public event OnCountDownEndedEventHandler CountDownEnded;
        private DispatcherTimer _timer;
        private TimeSpan _time;

        protected virtual void OnCountDownEnded() {
            CountDownEnded?.Invoke(this, EventArgs.Empty);
        }

        
        private void CountDown(object sender, RoutedEventArgs e) {
            
            _time = TimeSpan.FromSeconds(5);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, Callback , Application.Current.Dispatcher);

            _timer.Start();
            
        }
        private void Callback(object? sender, EventArgs e) {
            Time.Text = _time.ToString("c");
            
            if (_time == TimeSpan.Zero) {
                _timer.Stop();
                OnCountDownEnded();
            }
            PomodoroTimer pomodoroTimer = new PomodoroTimer();
            pomodoroTimer.OnCountDownEnded += (o, args) => { StartButton.IsEnabled = true;};

            _time = _time.Add(TimeSpan.FromSeconds(-1));
        }
        
    }
    
}