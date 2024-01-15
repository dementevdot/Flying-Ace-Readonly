using System;
using System.Linq;
using Data.Serializables;
using Localization;
using UniRx;
using UnityEngine;
using PlayerPrefs = Data.ReactivePlayerPrefs;

namespace Data.SO
{
    public class Game : ScriptableObject
    {
        public ReadOnlyReactiveProperty<uint> Coins;
        public ReadOnlyReactiveProperty<SkinName> CurrentSkin;
        public ReadOnlyReactiveProperty<uint> ShootDelayUpgrade;
        public ReadOnlyReactiveProperty<Language> Language;

        private ReactiveProperty<uint> _coins;
        private ReactiveCollection<LevelData> _unlockedLevels;
        private ReactiveCollection<SkinName> _unlockedSkins;
        private ReactiveProperty<SkinName> _currentSkin;
        private ReactiveProperty<Language> _language;
        private ReactiveProperty<uint> _shootDelayUpgrade;

        public event Action Inited; 

        public float ShootDelay => 1f / (Constants.DefaultShootsPerSecond + Constants.ShootUpgradeStep * _shootDelayUpgrade.Value);

        public float ShootsPerSecond => Constants.DefaultShootsPerSecond + Constants.ShootUpgradeStep * _shootDelayUpgrade.Value;

        public bool IsInited => Coins != null;

        public void Init()
        {
            _coins = new ReactiveProperty<uint>();
            _unlockedLevels = new ReactiveCollection<LevelData>();
            _unlockedSkins = new ReactiveCollection<SkinName>();
            _currentSkin = new ReactiveProperty<SkinName>();
            _language = new ReactiveProperty<Language>();
            _shootDelayUpgrade = new ReactiveProperty<uint>();

            Coins = new ReadOnlyReactiveProperty<uint>(_coins);
            CurrentSkin = new ReadOnlyReactiveProperty<SkinName>(_currentSkin);
            ShootDelayUpgrade = new ReadOnlyReactiveProperty<uint>(_shootDelayUpgrade);
            Language = new ReadOnlyReactiveProperty<Language>(_language);

            if (PlayerPrefs.HasKey(nameof(_coins)) == false)
            {
                LinkPlayerPrefs();
                SetPlayerPrefs();
            }
            else
            {
                GetPlayerPrefs();
                LinkPlayerPrefs();
            }

            Inited?.Invoke();
        }

        public void AddCoins(uint coins) => _coins.Value += coins;

        public void UnlockLevel(uint levelIndex)
        {
            if (IsLevelUnlocked(levelIndex) == false)
                _unlockedLevels.Add(new LevelData(levelIndex, 0));
        }

        public void SetLevelValues(uint levelIndex, uint score)
        {
            if (TryGetLevel(levelIndex, out LevelData level) == false) throw new InvalidOperationException();

            level.UpdateValues(score);
            PlayerPrefs.SetString(nameof(_unlockedLevels), JsonUtility.ToJson(new SerializableList<LevelData>(_unlockedLevels.ToList())));
        }

        public bool TryBuySkin(Skin skin)
        {
            if (TrySpendCoins(skin.Price) == false) return false;

            UnlockSkin(skin.SkinName);
            return true;
        }

        public bool TryUpgradeShootDelay()
        {
            if (_shootDelayUpgrade.Value >= Constants.MaxShootDelayUpgrade) return false;
            if (TrySpendCoins(Constants.ShootDelayUpgradePrice) == false) return false;

            UpgradeShootDelay();

            return true;
        }

        public void SetCurrentSkin(SkinName skinToSet)
        {
            if (IsSkinUnlocked(skinToSet) == false) throw new InvalidOperationException();

            _currentSkin.Value = skinToSet;
        }

        public bool IsSkinUnlocked(SkinName skinToCheck) => _unlockedSkins.Contains(skinToCheck);

        public bool IsLevelUnlocked(uint levelIndex) => TryGetLevel(levelIndex, out var useless);

        public uint GetLevelScore(uint levelIndex)
        {
            if (TryGetLevel(levelIndex, out LevelData level) == false) throw new InvalidOperationException();

            return level.Score;
        }

        private void LinkPlayerPrefs()
        {
            _coins.Subscribe(_ => PlayerPrefs.SetInt(nameof(_coins), (int)_coins.Value));
            _unlockedSkins.ObserveAdd().Subscribe(_ => 
                { PlayerPrefs.SetString(nameof(_unlockedLevels), JsonUtility.ToJson(new SerializableList<LevelData>(_unlockedLevels.ToList()))); });
            _unlockedSkins.ObserveAdd().Subscribe(_ => 
                { PlayerPrefs.SetString(nameof(_unlockedSkins), JsonUtility.ToJson(new SerializableList<SkinName>(_unlockedSkins.ToList()))); });
            _currentSkin.Subscribe(_ => PlayerPrefs.SetInt(nameof(_currentSkin), (int)_currentSkin.Value));
            _language.Subscribe(_ => PlayerPrefs.SetInt(nameof(_language), (int)_language.Value));
            _shootDelayUpgrade.Subscribe(_ => PlayerPrefs.SetInt(nameof(_shootDelayUpgrade), (int)_shootDelayUpgrade.Value));
        }

        private void SetPlayerPrefs()
        {
            _coins.Value = _coins.Value;
            _unlockedLevels.Add(new LevelData(0,0));
            _unlockedSkins.Add(SkinName.Default);
            _currentSkin.Value = _currentSkin.Value;
            _language.Value = _language.Value;
            _shootDelayUpgrade.Value = _shootDelayUpgrade.Value;
        }

        private void GetPlayerPrefs()
        {
            _coins.Value = (uint)PlayerPrefs.GetInt(nameof(_coins));
            _unlockedLevels = new(JsonUtility.FromJson<SerializableList<LevelData>>(PlayerPrefs.GetString(nameof(_unlockedLevels))).Value);
            _unlockedSkins = new(JsonUtility.FromJson<SerializableList<SkinName>>(PlayerPrefs.GetString(nameof(_unlockedSkins))).Value);
            _currentSkin.Value = Enum.Parse<SkinName>(PlayerPrefs.GetInt(nameof(_currentSkin)).ToString());
            _language.Value = Enum.Parse<Language>(PlayerPrefs.GetInt(nameof(_language)).ToString());
            _shootDelayUpgrade.Value = (uint)PlayerPrefs.GetInt(nameof(_shootDelayUpgrade));
        }

        private bool TrySpendCoins(uint coins)
        {
            if (_coins.Value < coins) return false;

            _coins.Value -= coins;
            return true;
        }

        private bool TryGetLevel(uint levelIndex, out LevelData levelData)
        {
            levelData = _unlockedLevels.FirstOrDefault(lvl => lvl.LevelIndex == levelIndex);

            return levelData != null;
        }

        private void UnlockSkin(SkinName skinToUnlock)
        {
            if (_unlockedSkins.Contains(skinToUnlock) == true) throw new InvalidOperationException();

            _unlockedSkins.Add(skinToUnlock);
        }

        private void UpgradeShootDelay() => _shootDelayUpgrade.Value++;
    }
}