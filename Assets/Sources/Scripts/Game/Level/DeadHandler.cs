using System.Collections;
using Agava.YandexGames;
using Game.Player;
using Shared;
using UI;
using UniRx;
using UnityEngine;

namespace Game.Level
{
    public class DeadHandler : MonoBehaviour
    {
        private static UI.Dead.ReviveButton _reviveButton;
        private static UI.Dead.Handler _deadHandler;
        private static UI.Complete.DoubleMoneyButton _doubleMoneyButton;

        private readonly CompositeDisposable _disposable = new ();

        [SerializeField] private Builder _builder;
        [SerializeField] private Data.SO.Game _data;

        private ScreenHandler _screenHandler;
        private Data.SO.Level _level;
        private Coroutine _playerDiedCoroutine;
        private uint _lastScore;
        private uint _lastCoinsReward;
        private bool _endless;
        private bool _coinsDoubled;

        private Health Health => _builder.Player.Health;
        private Points Points => _builder.Player.Points;

        private void OnDestroy()
        {
            _disposable.Clear();

            _reviveButton.OnRewarded -= Revive;

            if (_coinsDoubled == false)
                _doubleMoneyButton.OnRewarded -= DoubleMoney;
        }

        public void Init(ScreenHandler screenHandler, bool endless, Data.SO.Level level)
        {
            _screenHandler = screenHandler;
            _endless = endless;
            _level = level;

            if (_reviveButton == null || _deadHandler == null || _doubleMoneyButton == null)
            {
                var deadScreen = _screenHandler.GetScreen(State.Dead);

                _reviveButton = deadScreen.GetComponentInChildren<UI.Dead.ReviveButton>();
                _deadHandler = deadScreen.GetComponentInChildren<UI.Dead.Handler>();
                _doubleMoneyButton = _deadHandler.DoubleMoney;
            }

            PlayerValuesLink();

            _reviveButton.OnRewarded += Revive;
        }

        private void PlayerValuesLink()
        {
            Health.IsDead.Subscribe(isDead => { if (isDead == true) OnPlayerDied(); }).AddTo(_disposable);
        }

        private void OnPlayerDied()
        {
            if (_playerDiedCoroutine != null)
                StopCoroutine(_playerDiedCoroutine);

            _doubleMoneyButton.OnRewarded += DoubleMoney;

            _playerDiedCoroutine = StartCoroutine(PlayerDiedCoroutine());
        }

        private IEnumerator PlayerDiedCoroutine()
        {
            yield return new WaitForEndOfFrame();

            var points = Points.CurrentPoints.Value;

            uint coinsReward = _endless ? points / 2 : 10;

            _lastCoinsReward = coinsReward;

            _data.AddCoins(coinsReward);
            _deadHandler.SetValues(_endless);
            _deadHandler.SetCoins(coinsReward);
            _deadHandler.SetScore(points);

            if (_endless == true && YandexGamesSdk.IsRunningOnYandex == true)
            {
                var leaderboardScore = PlayerPrefs.GetInt(_level.LeaderboardKey, 0);

                if (points > leaderboardScore)
                    Leaderboard.SetScore(_level.LeaderboardKey, (int)points);

                var playScore = (int)(points - _lastScore);

                Leaderboard.GetPlayerEntry(Constants.MainLeaderboard, response =>
                {
                    var newScore = response.score + playScore;
                    print(newScore);
                    Leaderboard.SetScore(Constants.MainLeaderboard, newScore);
                });
            }

            _lastScore = Points.CurrentPoints.Value;

            yield return new WaitForSeconds(Constants.OpenScreenDelay);

            UIState.ChangeState(State.Dead);
        }

        private void Revive()
        {
            _disposable.Clear();

            _doubleMoneyButton.OnRewarded -= DoubleMoney;

            _builder.SpawnPlayer(50, _lastScore);

            PlayerValuesLink();

            UIState.ChangeState(State.Game);
        }

        private void DoubleMoney()
        {
            _doubleMoneyButton.OnRewarded -= DoubleMoney;

            _coinsDoubled = true;

            _data.AddCoins(_lastCoinsReward);
            _deadHandler.SetCoins(_lastCoinsReward * 2);
        }
    }
}