using Initializables;
using UniRx;
using UnityEngine;

namespace Shared
{
    public class AudioHandler : MonoBehaviour
    {
        private static readonly FloatReactiveProperty _volume = new ();

        private const string AudioVolume = "AudioVolume";

        [SerializeField] private YandexCloudSave _cloudSave;

        private bool _pausedByBackground = false;

        public static ReadOnlyReactiveProperty<float> Volume => new (_volume);

        private void Awake()
        {
            Agava.WebUtility.WebApplication.InBackgroundChangeEvent += inBackground =>
            {
                if (AudioListener.pause == true && _pausedByBackground == false) return;

                AudioListener.pause = inBackground;
                _pausedByBackground = inBackground;
            };

            DontDestroyOnLoad(this);

            _cloudSave.IsInited.Subscribe(isInited =>
            {
                if (isInited == false) return;

                AudioListener.volume = PlayerPrefs.GetFloat(AudioVolume, 1);
                _volume.Value = AudioListener.volume;
                _volume.Subscribe(volume => PlayerPrefs.SetFloat(AudioVolume, volume));
            });
        }

        public static void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            _volume.Value = volume;
        }
    }
}
