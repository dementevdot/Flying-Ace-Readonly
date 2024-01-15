using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Complete
{
    public class ScoreStar : MonoBehaviour
    {
        private readonly BoolReactiveProperty _isOn = new ();

        [SerializeField] private Image _starImage;
        [SerializeField] private ParticleSystem _starParticle;

        private void Awake()
        {
            _isOn.Subscribe(isOn =>
            {
                _starImage.color = isOn ? Color.white : Color.black;

                _starParticle.gameObject.SetActive(isOn);
            });
        }

        public void SetState(bool isOn) => _isOn.Value = isOn;
    }
}