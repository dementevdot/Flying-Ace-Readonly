using Agava.YandexGames;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class VideoAdButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        private bool _clicked;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            if (YandexGamesSdk.IsRunningOnYandex == true)
            {
                if (_clicked == true) return;

                _clicked = true;

                VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
            }
            else
            {
                OnRewardedCallback();
            }
        }

        protected virtual void OnCloseCallback()
        {
            _clicked = false;
            TimeHandler.ResumeTime();
            AudioListener.pause = false;
        }

        protected abstract void OnRewardedCallback();

        protected void OnOpenCallback()
        {
            TimeHandler.StopTime();
            AudioListener.pause = true;
        }

        private void OnErrorCallback(string obj)
        {
            OnCloseCallback();
        }
    }
}
