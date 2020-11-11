using System;
using System.Collections.Generic;
using System.Text;

namespace Pomodoro {
    public class PomodoroDuration {
        public static Dictionary<PomodoroState, TimeSpan> Durations = new Dictionary<PomodoroState, TimeSpan>() {
            { PomodoroState.None, TimeSpan.Zero },
            { PomodoroState.Focus, TimeSpan.FromMinutes(25) },
            { PomodoroState.ShortRest, TimeSpan.FromMinutes(5) },
            { PomodoroState.LongRest, TimeSpan.FromMinutes(15)}
        };
    }
}