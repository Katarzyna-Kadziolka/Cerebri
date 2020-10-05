using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Cerebri
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private PomodoroTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void CreateCountDown(object sender, RoutedEventArgs e) {
            timer = new PomodoroTimer();
            timer.SetTime(TimeSpan.FromSeconds(5));
            timer.Start();
            StartButton.IsEnabled=false;
            timer.OnTick += (o, args) => Time.Text = args.TimeToEnd.ToString("c");
            timer.OnCountDownEnded += (o, args) => { StartButton.IsEnabled = true; };
        }

        
    }
}
