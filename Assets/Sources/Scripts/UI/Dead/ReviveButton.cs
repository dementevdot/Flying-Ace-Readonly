using System;
using Agava.YandexGames;

namespace UI.Dead
{
    public class ReviveButton : VideoAdButton
    {
        private bool _isRewarded;

        public event Action OnRewarded;

        protected override void OnRewardedCallback()
        {
            _isRewarded = true;

            if (YandexGamesSdk.IsRunningOnYandex == false)
                OnRewarded?.Invoke();
        }

        protected override void OnCloseCallback()
        {
            if (_isRewarded == false) return;

            _isRewarded = false;

            OnRewarded?.Invoke();

            base.OnCloseCallback();
        }
    }
}