using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

		private PomodoroTimer _timer = new PomodoroTimer();
		private int _pomodoroNumber = 1;
		private PomodoroStateManager _stateManager = new PomodoroStateManager();

		public MainWindow() {
			InitializeComponent();
			this.DataContext = this;
		}

		private void StartButton_OnClick(object sender, RoutedEventArgs e) {
			switch (_timer.TimerState) {
				case TimerState.Waiting:
					StartNewTimer();
					break;
				case TimerState.Paused:
					StartTimer();
					break;
				case TimerState.Ongoing:
					PauseTimer();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void StartNewTimer() {
			StartButton.Content = "Pause";
			_stateManager.GoToNextState();
			CreatePomodoro(PomodoroDuration.Durations[_stateManager.CurrentState]);
			PomodoroNumber = _stateManager.PomodoroCount;
		}

		private void StartTimer() {
			StartButton.Content = "Pause";
			_timer.Start();

		}

		private void PauseTimer() {
			StartButton.Content = "Start";
			_timer.Stop();
		}

		private void CreatePomodoro(TimeSpan time) {
			_timer = new PomodoroTimer();
			_timer.SetTime(time);
			_timer.Start();
			_timer.OnTick += (o, args) => TimeTextBlock.Text = args.TimeToEnd.ToString("c");
			_timer.OnCountDownEnded += (o, args) => { StartButton.Content = "Start"; };
			_timer.OnCountDownEnded += (o, args) => {
				TimeTextBlock.Text = PomodoroDuration.Durations[_stateManager.GetNextState()].ToString("c");
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void AddTaskButton_OnClick(object sender, RoutedEventArgs e) {
			List<Tasks> items = new List<Tasks>();
			items.Add(new Tasks() {Task = new TextBlock() {Text = TaskEntry.Text}, Checkbox = new CheckBox(), DeleteButton = new Button()});
			TasksListView.ItemsSource = items;

		}
	}
}
