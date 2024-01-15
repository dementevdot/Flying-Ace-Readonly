using TMPro;
using UnityEngine;

namespace UI.Complete
{
    public class Handler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private ScoreStar[] _scoreStars;

        public void SetCoins(uint coins) => _coins.text = coins.ToString();
        public void SetScore(uint score) => _score.text = score.ToString();

        public void SetStars(uint stars)
        {
            for (int i = 0; i < _scoreStars.Length; i++)
                _scoreStars[i].SetState(i + 1 <= stars);
        }
    }
}
