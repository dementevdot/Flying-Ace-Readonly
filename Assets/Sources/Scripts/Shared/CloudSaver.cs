using Agava.YandexGames;
using Data.Serializables;
using UnityEngine;
using PlayerPrefs = Data.ReactivePlayerPrefs;

namespace Shared
{
    public class CloudSaver : MonoBehaviour
    {
        private string _prevPrefs;

        private void Start()
        {
            if (YandexGamesSdk.IsRunningOnYandex == true)
                PlayerPrefs.PlayerPrefsChanged += CloudSave;
        }

        private void CloudSave()
        {
            var currentPrefs = new PlayerPrefsRouter().GetJSON();

            if (currentPrefs == _prevPrefs) return;

            PlayerAccount.SetCloudSaveData(new PlayerPrefsRouter().GetJSON());
            _prevPrefs = currentPrefs;
        }
    }
}
