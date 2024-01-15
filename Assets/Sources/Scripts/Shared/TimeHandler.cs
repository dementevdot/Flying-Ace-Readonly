using UnityEngine;

namespace Shared
{
    public class TimeHandler : MonoBehaviour
    {
        [SerializeField] private float _timeHack;

        public static bool TimeStopped => Time.timeScale == 0;

        private void Awake()
        {
            UIState.StateChanged += OnGlobalStateChanged;
        }

        private void OnDestroy()
        {
            UIState.StateChanged -= OnGlobalStateChanged;
        }

        private void OnValidate()
        {
            Time.timeScale = _timeHack;
            Time.fixedDeltaTime = _timeHack * 0.02f;
        }

        public static void StopTime()
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }

        public static void ResumeTime()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }

        private void OnGlobalStateChanged(State current, State next)
        {
            if (current == State.Game && next is State.Complete or State.Dead or State.Pause)
            {
                Time.timeScale = 0;
                return;
            }

            if (Time.timeScale == 0 && next != State.Complete && next != State.Dead && next != State.Pause)
            {
                Time.timeScale = 1;
            }
        }
    }
}