using System;
using Data.SO;
using UI;
using UnityEngine;

namespace Shared
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _menuBackground;
        [SerializeField] private Game.Level.Builder _levelBase;
        [SerializeField] private Levels _levels;
        [SerializeField] private ScreenHandler _screenHandler;
        [SerializeField] private Camera _camera;

        private GameObject _loadedMenuBackground;
        private GameObject _loadedLevel;
        private uint _loadedLevelIndex;

        public static event Action<uint, Level, bool> LevelLoaded;

        private void Awake()
        {
            LoadMenuBackground();

            UIState.StateChanged += OnGlobalStateChanged;
        }

        private void OnDestroy()
        {
            UIState.StateChanged -= OnGlobalStateChanged;
        }

        public void LoadLevel(uint levelIndex, bool endless = false)
        {
            _loadedLevelIndex = levelIndex;

            InstantiateLevel(levelIndex, endless);

            LevelLoaded?.Invoke(levelIndex, _levels.GetLevel(levelIndex), endless);

            if (_loadedMenuBackground != null)
                Destroy(_loadedMenuBackground);
        }

        public void ReloadLevel()
        {
            UnloadLevel();
            LoadLevel(_loadedLevelIndex);
        }

        public void LoadNextLevel()
        {
            UnloadLevel();
            LoadLevel(_loadedLevelIndex + 1);
        }

        public bool LoadedLevelIsLast() => _loadedLevelIndex + 1 == _levels.LevelsCount;

        private void OnGlobalStateChanged(State current, State next)
        {
            if (next != State.Start || _loadedLevel == null) return;

            UnloadLevel();
            LoadMenuBackground();
        }

        private void UnloadLevel() => Destroy(_loadedLevel);

        private void InstantiateLevel(uint levelIndex, bool endless)
        {
            var level = _levels.GetLevel(levelIndex);

            _loadedLevel = Instantiate(_levelBase.gameObject);
            _loadedLevel.GetComponent<Game.Level.Builder>().Init(level, levelIndex, endless, _camera, _screenHandler);
        }

        private void LoadMenuBackground()
        {
            if (_loadedMenuBackground != null) throw new InvalidOperationException();

            _loadedMenuBackground = Instantiate(_menuBackground);
        }
    }
}
