using System.Linq;
using Shared;
using UnityEngine;

namespace UI
{
    public class ScreenHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] _screens;
        [SerializeField] private StateButtonPair[] _stateButtonPairs;

        private void Awake()
        {
            foreach (var screen in _screens)
                screen.SetActive(true);

            foreach (var stateButtonPair in _stateButtonPairs)
                stateButtonPair.Init();
        }

        public GameObject GetScreen(State state) => _screens.First(go => go.name == state.ToString());
    }
}
