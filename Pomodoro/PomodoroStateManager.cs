using System;

namespace Pomodoro {
    public class PomodoroStateManager {
        public int PomodoroCount { get; private set; } = 0;
        public PomodoroState CurrentState { get; private set; }

        public void GoToNextState() {
            if (CurrentState == PomodoroState.LongRest) {
                PomodoroCount = 0;
            }
            CurrentState = GetNextState();
            if (CurrentState == PomodoroState.Focus) {
                PomodoroCount++;
            }
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