using UnityEngine;
using UnityEngine.UI;

namespace Develop
{
    public class PlayModeCounter : MonoBehaviour
    {
        private const string PlayModeCount = "PlayModeCount";

        [SerializeField] private int _count;
        [SerializeField] private Text _text;

        private void OnValidate()
        {
            _count = PlayerPrefs.GetInt(PlayModeCount);

            if (_text != null)
                _text.text = $"1.{_count}";
        }

#if UNITY_EDITOR
        private void Awake()
        {
            if (_text != null) return;

            if (_count > PlayerPrefs.GetInt(PlayModeCount))
                PlayerPrefs.SetInt(PlayModeCount, _count);

            PlayerPrefs.SetInt(PlayModeCount, PlayerPrefs.GetInt(PlayModeCount) + 1);
        }
#endif
    }
}
