using Shared;
using UnityEngine;

namespace UI
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private bool _invokeByState = true;
        [SerializeField] private State _invokeState;

        private void Awake()
        {
            if (_invokeByState != true) return;

            UIState.StateChanged += OnGlobalStateChanged;

            gameObject.SetActive(UIState.CurrentState == _invokeState);
        }

        private void OnDestroy()
        {
            if (_invokeByState == true)
                UIState.StateChanged -= OnGlobalStateChanged;
        }

        private void OnGlobalStateChanged(State current, State next)
        {
            if (next == _invokeState)
                gameObject.SetActive(true);
            else if (gameObject.activeSelf == true)
                    gameObject.SetActive(false);
        }
    }
}