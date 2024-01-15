using System;

namespace UI.Complete
{
    public class DoubleMoneyButton : VideoAdButton
    {
        public event Action OnRewarded;

        private void OnEnable()
        {
            _button.interactable = true;
        }

        protected override void OnRewardedCallback()
        {
            _button.interactable = false;
            OnRewarded?.Invoke();
        }
    }
}