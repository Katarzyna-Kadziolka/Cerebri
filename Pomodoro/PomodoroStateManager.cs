using System;

namespace Pomodoro {
    public class PomodoroStateManager {
        public int PomodoroCount { get; private set; }
        public PomodoroState CurrentState { get; private set; }

        public void GoToNextState() {
            if (CurrentState == PomodoroState.LongRest) {
                PomodoroCount = 1;
            }
            CurrentState = GetNextState();
        }

        public PomodoroState GetNextState() {
            PomodoroState nextPomodoroState = PomodoroState.None;
            switch (CurrentState) {
                case PomodoroState.None:
                    nextPomodoroState = PomodoroState.Focus;
                    break;
                case PomodoroState.Focus:
                    if (PomodoroCount == 4) {
                        nextPomodoroState = PomodoroState.LongRest;
                    }
                    else {
                        nextPomodoroState = PomodoroState.ShortRest;
                    }
                    break;
                case PomodoroState.ShortRest:
                    nextPomodoroState = PomodoroState.Focus;
                    break;
                case PomodoroState.LongRest:
                    nextPomodoroState = PomodoroState.Focus;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return nextPomodoroState;
        }
    }
}