using System;
using System.Collections.Generic;
using System.Text;

namespace Pomodoro {
    public class PomodoroDuration {
        public static Dictionary<PomodoroState, TimeSpan> Durations = new Dictionary<PomodoroState, TimeSpan>() {
            { PomodoroState.None, TimeSpan.Zero },
            { PomodoroState.Focus, TimeSpan.FromSeconds(7) },
            { PomodoroState.ShortRest, TimeSpan.FromSeconds(3) },
            { PomodoroState.LongRest, TimeSpan.FromSeconds(5)}
        };
    }
}