using System;

namespace Pomodoro {
    public class TickEventArgs : EventArgs {
        public TimeSpan TimeToEnd { get; set; }
        
    }
}