using System;
using System.Collections;
using Agava.YandexGames;
using Game.Player;
using Shared;
using UI;
using UnityEngine;

namespace Game.Level
{
    public class CompleteHandler : MonoBehaviour
    {
        private static UI.Complete.Handler _completeHandler;
        private static UI.Complete.DoubleMoneyButton _doubleMoneyButton;

        [SerializeField] private Builder _builder;
        [SerializeField] private Data.SO.Game _data;

        private ScreenHandler _screenHandler;
        private Data.SO.Level _currentLevel;
        private uint _currentLevelIndex;
        private uint _lastCoinsReward;
        private bool _coinsDoubled;

        private Coroutine _levelCompleteCoroutine;

        private Points Points => _builder.Player.Points;

        private void OnDestroy()
        {
            if (_coinsDoubled == false)
                _doubleMoneyButton.OnRewarded -= DoubleMoney;
        }

        public void Init(ScreenHandler screenHandler, uint currentLevelIndex, Data.SO.Level level)
        {
            _screenHandler = screenHandler;
            _currentLevelIndex = currentLevelIndex;
            _currentLevel = level;

            if (_completeHandler == null || _doubleMoneyButton == null)
            {
                var screen = _screenHandler.GetScreen(State.Complete);

                _completeHandler = screen.GetComponent<UI.Complete.Handler>();
                _doubleMoneyButton = screen.GetComponentInChildren<UI.Complete.DoubleMoneyButton>();
            }

            Enemy.Health.BossDied += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            Enemy.Health.BossDied -= OnLevelComplete;

            _doubleMoneyButton.OnRewarded += DoubleMoney;

            if (_levelCompleteCoroutine != null) throw new InvalidOperationException();

            _levelCompleteCoroutine = StartCoroutine(LevelCompleteCoroutine());
        }

        private IEnumerator LevelCompleteCoroutine()
        {
            yield return new WaitForEndOfFrame();

            var points = Points.CurrentPoints.Value;

            if (YandexGamesSdk.IsRunningOnYandex == true)
            {
                Leaderboard.GetPlayerEntry(Constants.MainLeaderboard, response =>
                {
                    Leaderboard.SetScore(Constants.MainLeaderboard, response.score + (int)points);
                });
            }

            uint coinsReward = (uint)(points / 1.5f);

            _lastCoinsReward = coinsReward;

            var starsCount = _currentLevel.GetStarsCount(points);

            _completeHandler.SetCoins(coinsReward);
            _completeHandler.SetScore(points);
            _completeHandler.SetStars(starsCount);

            _data.UnlockLevel(_currentLevelIndex + 1);
            _data.SetLevelValues(_currentLevelIndex, points);
            _data.AddCoins(coinsReward);

            yield return new WaitForSeconds(Constants.OpenScreenDelay);

            UIState.ChangeState(State.Complete);
        }

        private void DoubleMoney()
        {
            _doubleMoneyButton.OnRewarded -= DoubleMoney;

            _coinsDoubled = true;

            _data.AddCoins(_lastCoinsReward);
            _completeHandler.SetCoins(_lastCoinsReward * 2);
        }
    }
}