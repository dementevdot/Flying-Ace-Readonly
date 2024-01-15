using Data.SO;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class BuySkinButton : MonoBehaviour
    {
        private readonly BoolReactiveProperty _unlocked = new ();

        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private Skin _skin;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _priceObject;
        [SerializeField] private TMP_Text _priceText;

        private void Awake()
        {
            _unlocked.Subscribe(isUnlocked => _priceObject.SetActive(isUnlocked == false));

            if (_data.IsInited == false)
                _data.Inited += Init;
            else
                Init();
        }

        private void Init()
        {
            _unlocked.Value = _data.IsSkinUnlocked(_skin.SkinName);
            _priceText.text = _skin.Price.ToString();

            _button.onClick.AddListener(OnClick);
            _data.CurrentSkin.Subscribe(currentSkin => _button.interactable = currentSkin != _skin.SkinName);
        }

        private void OnClick()
        {
            if (_unlocked.Value == false)
                _unlocked.Value = _data.TryBuySkin(_skin);
            else
                _data.SetCurrentSkin(_skin.SkinName);
        }
    }
}
