using Agava.WebUtility;
using Shared;
using UnityEngine;

namespace UI.Game
{
    public class PauseOnBackground : MonoBehaviour
    {
        private void Awake()
        {
            WebApplication.InBackgroundChangeEvent += inBackground =>
            {
                if (TimeHandler.TimeStopped == true) return;

                if (UIState.CurrentState == State.Game && inBackground == true)
                    UIState.ChangeState(State.Pause);
            };
        }
    }
}