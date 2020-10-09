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

		private PomodoroTimer timer;
		private int _pomodoroNumber = 10;

		public MainWindow() {
			InitializeComponent();
			this.DataContext = this;
		}


		private void CreateCountDown(object sender, RoutedEventArgs e) {
			timer = new PomodoroTimer();
			timer.SetTime(TimeSpan.FromSeconds(5));
			timer.Start();
			StartButton.IsEnabled = false;
			timer.OnTick += (o, args) => Time.Text = args.TimeToEnd.ToString("c");
			timer.OnCountDownEnded += (o, args) => { StartButton.IsEnabled = true; };
			timer.OnCountDownEnded += CountPomodoro;
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