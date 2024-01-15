using Localization;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Leaderboard
{
    public class Cell : MonoBehaviour
    {
        private const string AnonRu = "Аноним";
        private const string AnonEn = "Anonymous";
        private const string AnonTr = "Anonim";

        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void Init(int rank, string playerName, int score)
        {
            _rank.text = rank.ToString();
            _score.text = score.ToString();

            if (string.IsNullOrEmpty(playerName))
            {
                OnLanguageChanged(_data.Language.Value);
                _data.Language.Subscribe(OnLanguageChanged);

                return;
            }

            _name.text = playerName;
        }

        private void OnLanguageChanged(Language language)
        {
            _name.text = language switch
            {
                Language.Russian => AnonRu,
                Language.English => AnonEn,
                Language.Turkish => AnonTr,
                _ => _name.text
            };
        }
    }
}