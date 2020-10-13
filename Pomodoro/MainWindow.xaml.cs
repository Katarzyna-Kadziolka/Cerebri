using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using Pomodoro.Annotations;

namespace Cerebri {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged {
		public int PomodoroNumber {
			get { return _pomodoroNumber; }
			set {
				if (value != _pomodoroNumber) {
					_pomodoroNumber = value;
					OnPropertyChanged(nameof(PomodoroNumber));
				}
			}
		}
        public int Time {
            get { return _time; }
            set {
                if (value != _time) {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        private PomodoroTimer timer;
		private int _pomodoroNumber = 1;
        private int _time = 25;
        private PomodoroState _nextPomodoroState = PomodoroState.Focus;
        private PomodoroState _pomodoroState = PomodoroState.None;
		public MainWindow() {
			InitializeComponent();
			this.DataContext = this;
		}

		private void CreateCountDown(object sender, RoutedEventArgs e) {
            switch (_pomodoroState) {
                case PomodoroState.None:
                    _pomodoroState = PomodoroState.Focus;
                    CreatePomodoro(PomodoroDuration.Durations[PomodoroState.Focus]);
                    timer.OnCountDownEnded += CountPomodoro;
                    _nextPomodoroState = PomodoroState.ShortRest;
                    break;
                case PomodoroState.Focus:
                    _pomodoroState = PomodoroState.ShortRest;
                    CreatePomodoro(PomodoroDuration.Durations[PomodoroState.ShortRest]);
                    _nextPomodoroState = PomodoroState.Focus;
                    break;
                case PomodoroState.ShortRest:
                    _pomodoroState = PomodoroState.Focus;
                    CreatePomodoro(PomodoroDuration.Durations[PomodoroState.Focus]);
                    timer.OnCountDownEnded += CountPomodoro;
                    _nextPomodoroState = PomodoroState.ShortRest;
                    break;
                case PomodoroState.LongRest:
                    _pomodoroState = PomodoroState.Focus;
                    CreatePomodoro(PomodoroDuration.Durations[PomodoroState.Focus]);
                    timer.OnCountDownEnded += CountPomodoro;
                    _nextPomodoroState = PomodoroState.ShortRest;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreatePomodoro(TimeSpan time) {
            timer = new PomodoroTimer();
            timer.SetTime(time);
            timer.Start();
            StartButton.IsEnabled = false;
            timer.OnTick += (o, args) => TimeTextBlock.Text = args.TimeToEnd.ToString("c");
            timer.OnCountDownEnded += (o, args) => { StartButton.IsEnabled = true; };
            timer.OnCountDownEnded += (o, args) => { TimeTextBlock.Text = PomodoroDuration.Durations[_nextPomodoroState].ToString("c"); };
        }


        private void CountPomodoro(object? sender, EventArgs e) {
			PomodoroNumber += 1;
		}


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}