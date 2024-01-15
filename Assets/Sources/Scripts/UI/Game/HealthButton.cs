using Agava.YandexGames;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class HealthButton : VideoAdButton
    {
        [SerializeField] private Gamebar _gamebar;
        [SerializeField] private TMP_Text _healthText;

        protected override void Awake()
        {
            base.Awake();

            _healthText.text = $"+{Constants.HealthForAdView}";
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (YandexGamesSdk.IsRunningOnYandex == true)
                OnOpenCallback();
        }

        protected override void OnRewardedCallback()
        {
            _gamebar.Health.ApplyAdViewHealth();
        }
    }
}