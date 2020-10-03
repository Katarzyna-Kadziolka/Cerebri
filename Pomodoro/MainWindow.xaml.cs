using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Pomodoro;

namespace Cerebri
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void CountDown(object sender, RoutedEventArgs e) {
            _time = TimeSpan.FromSeconds(5);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, Callback , Application.Current.Dispatcher);

            _timer.Start();
            StartButton.IsEnabled=false;
        }

        private void Callback(object? sender, EventArgs e) {
            Time.Text = _time.ToString("c");
            
            if (_time == TimeSpan.Zero) {
                _timer.Stop();
                StartButton.IsEnabled = true;
            }
            PomodoroTimer pomodoroTimer = new PomodoroTimer();
            pomodoroTimer.OnCountDownEnded += (o, args) => { StartButton.IsEnabled = true;};

            _time = _time.Add(TimeSpan.FromSeconds(-1));
        }

        private void MainWindow_OnCountDownEnded(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
