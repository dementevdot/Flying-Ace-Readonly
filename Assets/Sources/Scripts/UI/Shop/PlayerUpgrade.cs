using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UI.Shop
{
    public class PlayerUpgrade : MonoBehaviour
    {
        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _upgradeInput;

        private void Awake()
        {
            if (_data.IsInited == false)
                _data.Inited += Init;
            else
                Init();

            _upgradeButton.onClick.AddListener(Upgrade);
        }

        private void Upgrade()
        {
            _data.TryUpgradeShootDelay();
        }

        private void Init()
        {
            _data.ShootDelayUpgrade.Subscribe(_ =>
            {
                _upgradeInput.text = $"{_data.ShootsPerSecond}/{Constants.MaxShootDelayUpgrade * Constants.ShootUpgradeStep + Constants.DefaultShootsPerSecond}";
            });
            _price.text = Constants.ShootDelayUpgradePrice.ToString();
        }
    }
}
