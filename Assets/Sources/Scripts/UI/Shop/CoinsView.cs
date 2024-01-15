using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Shop
{
    public class CoinsView : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();

        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            _data.Coins?.Subscribe(coins => _text.text = coins.ToString()).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }
    }
}
