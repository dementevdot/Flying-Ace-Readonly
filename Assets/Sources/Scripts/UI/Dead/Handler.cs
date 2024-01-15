using System;
using TMPro;
using UI.Complete;
using UnityEngine;

namespace UI.Dead
{
    public class Handler : MonoBehaviour
    {
        [SerializeField] private GameObject _doubleMoneyButton;
        [SerializeField] private DoubleMoneyButton _doubleMoney;
        [SerializeField] private GameObject _reviveButton;
        [SerializeField] private GameObject _score;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _scoreText;

        public DoubleMoneyButton DoubleMoney => _doubleMoney;

        private void Awake() => _doubleMoney.OnRewarded += DisableButton;

        private void OnDestroy() => _doubleMoney.OnRewarded -= DisableButton;

        private void OnEnable() => _reviveButton.SetActive(true);

        public void SetValues(bool endless)
        {
            _doubleMoneyButton.SetActive(endless);
            _score.SetActive(endless);
        }

        public void SetCoins(uint coins) => _coinsText.text = coins.ToString();

        public void SetScore(uint score) => _scoreText.text = score.ToString();

        private void DisableButton() => _reviveButton.SetActive(false);
    }
}