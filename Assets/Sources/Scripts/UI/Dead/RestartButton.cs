using Shared;
using UnityEngine;

namespace UI.Dead
{
    public class RestartButton : InterstitialAdButton
    {
        [SerializeField] private LevelLoader _levelLoader;

        protected override void Awake()
        {
            base.Awake();
            _button.onClick.AddListener(_levelLoader.ReloadLevel);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _button.onClick.RemoveListener(_levelLoader.ReloadLevel);
        }
    }
}