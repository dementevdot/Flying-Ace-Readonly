using System;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class StateButtonPair
    {
        [SerializeField] private State _state;
        [SerializeField] private Button _button;

        public void Init()
        {
            _button.onClick.AddListener(() => UIState.ChangeState(_state));
        }
    }
}