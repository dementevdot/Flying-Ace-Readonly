using Agava.YandexGames;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InterstitialAdButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            if (YandexGamesSdk.IsRunningOnYandex == true)
                InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
            else
                OnCloseCallback(true);
        }

        protected virtual void OnCloseCallback(bool isValid)
        {
            if (isValid == false) return;

            TimeHandler.ResumeTime();
            AudioListener.pause = false;
        }

        private static void OnOpenCallback()
        {
            TimeHandler.StopTime();
            AudioListener.pause = true;
        }

        private void OnErrorCallback(string obj)
        {
            OnCloseCallback(true);
        }
    }
}
