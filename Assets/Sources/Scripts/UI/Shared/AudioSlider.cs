using Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shared
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            _slider.value = AudioHandler.Volume.Value;
            _slider.onValueChanged.AddListener(AudioHandler.SetVolume);
            AudioHandler.Volume.Subscribe(volume => _slider.value = volume);
        }
    }
}