using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Pomodoro;
using Pomodoro.Annotations;
using System.Linq;

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

        public int Progress {
            get { return _progress;  }
            set {
				if (value != _progress) {
                    _progress = value;
                }
            }
        }

		private ObservableCollection<ToDoTask> _tasksList = new ObservableCollection<ToDoTask>();
		private PomodoroTimer _timer = new PomodoroTimer();
		private int _pomodoroNumber = 1;
		private PomodoroStateManager _stateManager = new PomodoroStateManager();
        private int _progress = 0;
     
		public MainWindow() {
			InitializeComponent();
			this.DataContext = this;
            TasksListView.ItemsSource = _tasksList;
			
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
			_tasksList.Add(new ToDoTask() { Description = TaskEntry.Text});
			TaskEntry.Text = string.Empty;
		}


        private void RemoveButton_OnClick(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            _tasksList.Remove((ToDoTask)button.DataContext);
        }

        private double CountProgress() {
            int taskNumber = _tasksList.Count;
            int checkedBoxes = _tasksList.Count(task => task.IsDone);
            double progress = (double) checkedBoxes / taskNumber;
            return progress;
        }

        private void TaskCheckbox_OnClick(object sender, RoutedEventArgs e) {
	        ProgressBar.Value = CountProgress();
        }
	}
}
