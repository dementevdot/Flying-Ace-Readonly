using Agava.YandexGames;
using Game.Player;
using Shared;
using TMPro;
using UniRx;
using UnityEngine;
using Player = Game.Player;

namespace UI.Game
{
    public class Gamebar : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();

        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private TMP_Text _healhText;
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _bestLevelScoreText;

        private Health _health;
        private Points _points;

        public Health Health => _health;

        private void Awake()
        {
            Player.Main.PlayerCreated += (health, points) =>
            {
                _health = health;
                _points = points;

                _health.CurrentHealth.Subscribe(healthCount => _healhText.text = healthCount.ToString()).AddTo(_disposable);
                _points.CurrentPoints.Subscribe(pointsCount => _pointsText.text = pointsCount.ToString()).AddTo(_disposable);
            };

            LevelLoader.LevelLoaded += (levelIndex, level, endless) =>
            {
                if (endless == false)
                    _bestLevelScoreText.text = _data.GetLevelScore(levelIndex).ToString();
                else
                    _bestLevelScoreText.text = YandexGamesSdk.IsRunningOnYandex == true ? PlayerPrefs.GetInt(level.LeaderboardKey, 0).ToString() : "0";
            };
        }

        private void OnDestroy() => _disposable.Clear();
    }
}
