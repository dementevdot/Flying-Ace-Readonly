using System;
using System.Collections.Generic;
using Localization;
using UnityEngine;

namespace Data.Serializables
{
    [Serializable]
    public class PlayerPrefsRouter
    {
        [SerializeField] private uint _coins = 0;
        [SerializeField] private SerializableList<LevelData> _unlockedLevels = new (new List<LevelData> { new (0, 0) });
        [SerializeField] private SerializableList<SkinName> _unlockedSkins = new (new List<SkinName> { SkinName.Default });
        [SerializeField] private SkinName _currentSkin = SkinName.Default;
        [SerializeField] private Language _language = Language.Russian;
        [SerializeField] private uint _shootDelayUpgrade = 0;

        public void SetToPrefs()
        {
            PlayerPrefs.SetInt(nameof(_coins), (int)_coins);
            PlayerPrefs.SetString(nameof(_unlockedLevels), JsonUtility.ToJson(_unlockedLevels));
            PlayerPrefs.SetString(nameof(_unlockedSkins), JsonUtility.ToJson(_unlockedSkins));
            PlayerPrefs.SetInt(nameof(_currentSkin), (int)_currentSkin);
            PlayerPrefs.SetInt(nameof(_language), (int)_language);
            PlayerPrefs.SetInt(nameof(_shootDelayUpgrade), (int)_shootDelayUpgrade);
        }

        public void GetFromPrefs()
        {
            _coins = (uint)PlayerPrefs.GetInt(nameof(_coins));
            _unlockedLevels = JsonUtility.FromJson<SerializableList<LevelData>>(PlayerPrefs.GetString(nameof(_unlockedLevels)));
            _unlockedSkins = JsonUtility.FromJson<SerializableList<SkinName>>(PlayerPrefs.GetString(nameof(_unlockedSkins)));
            _currentSkin = Enum.Parse<SkinName>(PlayerPrefs.GetInt(nameof(_currentSkin)).ToString());
            _language = Enum.Parse<Language>(PlayerPrefs.GetInt(nameof(_language)).ToString());
            _shootDelayUpgrade = (uint)PlayerPrefs.GetInt(nameof(_shootDelayUpgrade));
        }

        public string GetJSON()
        {
            GetFromPrefs();
            return JsonUtility.ToJson(this);
        }

        public void SetLanguage(Language language) => _language = language;
    }
}