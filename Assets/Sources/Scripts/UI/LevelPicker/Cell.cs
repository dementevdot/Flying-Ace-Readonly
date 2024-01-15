using Agava.YandexGames;
using Data.SO;
using Localization;
using Shared;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelPicker
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private Levels _levels;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _leaderboardScore;
        [SerializeField] private Image _preview;
        [SerializeField] private RectTransform _difficultScale;
        [SerializeField] private Button _pickButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private GameObject[] _offEndlessObjects;
        [SerializeField] private GameObject[] _onEndlessObjects;
        [SerializeField] private Toggle _isEndless;
        [SerializeField] private Image[] _stars;

        private Level _level;
        private uint _levelIndex;
        private float _oneScaleSize;

        private void OnEnable()
        {
            if (_level == null) return;

            _difficultScale.sizeDelta = new Vector2(_level.Difficult * _oneScaleSize, _difficultScale.sizeDelta.y);

            bool isUnlocked = _data.IsLevelUnlocked(_levelIndex);

            _pickButton.interactable = isUnlocked;
            _isEndless.interactable = isUnlocked;

            foreach (var infinityObject in _onEndlessObjects)
                infinityObject.SetActive(_isEndless.isOn);

            foreach (var nonInfinityObject in _offEndlessObjects)
                nonInfinityObject.SetActive(_isEndless.isOn == false);

            var starsCount = isUnlocked ? _levels.GetLevel(_levelIndex).GetStarsCount(_data.GetLevelScore(_levelIndex)) : 0;

            for (int i = 0; i < _stars.Length; i++)
                _stars[i].enabled = i + 1 <= starsCount;
        }

        private void OnDisable()
        {
            _isEndless.isOn = false;
        }

        public void Init(uint levelIndex, LevelLoader levelLoader, Leaderboard.View leaderboard)
        {
            _levelIndex = levelIndex;
            _oneScaleSize = _difficultScale.sizeDelta.x;

            _level = _levels.GetLevel(_levelIndex);
            _preview.sprite = _level.Sprite;

            var levelName = _level.Name;

            _data.Language.Subscribe(language =>
            {
                _name.text = language switch
                {
                    Language.Russian => levelName.Russian,
                    Language.English => levelName.English,
                    Language.Turkish => levelName.Turkish,
                    _ => _name.text
                };
            });

            _pickButton.onClick.AddListener(() =>
            {
                levelLoader.LoadLevel(levelIndex, _isEndless.isOn);
                UIState.ChangeState(State.Game);
            });

            _leaderboardButton.onClick.AddListener(() =>
            {
                if (YandexGamesSdk.IsRunningOnYandex == true && PlayerAccount.IsAuthorized == true)
                {
                    Agava.YandexGames.Leaderboard.GetEntries(_level.LeaderboardKey, response =>
                    {
                        leaderboard.Init(response, _name.text);
                        UIState.ChangeState(State.Ranking);
                    });
                }
                else
                {
                    leaderboard.Init(null, _name.text);
                    UIState.ChangeState(State.Ranking);
                }
            });

            _isEndless.onValueChanged.AddListener(isEndless =>
            {
                foreach (var endlessObject in _onEndlessObjects)
                    endlessObject.SetActive(isEndless);

                foreach (var offEndlessObject in _offEndlessObjects)
                    offEndlessObject.SetActive(isEndless == false);

                if (_isEndless == true && YandexGamesSdk.IsRunningOnYandex == true)
                {
                    Agava.YandexGames.Leaderboard.GetPlayerEntry(_level.LeaderboardKey, response =>
                    {
                        _leaderboardScore.text = response.score.ToString();
                        PlayerPrefs.SetInt(_level.LeaderboardKey, response.score);
                    });
                }
            });

            OnEnable();
        }
    }
}
