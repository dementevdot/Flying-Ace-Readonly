using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Complete
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private Button _button;

        private void Awake() => _button.onClick.AddListener(_levelLoader.LoadNextLevel);

        private void OnEnable() => _button.interactable = _levelLoader.LoadedLevelIsLast() == false;

        private void OnDestroy() => _button.onClick.RemoveListener(_levelLoader.LoadNextLevel);
    }
}