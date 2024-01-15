using System.Collections;
using Agava.YandexGames;
using Data.Serializables;
using Localization;
using UnityEngine;

namespace Initializables
{
    public class YandexCloudSave : Base
    {
        private IEnumerator Start()
        {
            if (YandexGamesSdk.IsRunningOnYandex == false)
            {
                IsInited.Value = true;

                yield break;
            }

            while (YandexGamesSdk.IsInitialized == false)
                yield return new WaitForFixedUpdate();

            PlayerAccount.GetCloudSaveData(SetCloudSave);
        }

        private void SetCloudSave(string data)
        {
            if (data != "{}")
            {
                JsonUtility.FromJson<PlayerPrefsRouter>(data).SetToPrefs();
            }
            else
            {
                Language lang = YandexGamesSdk.Environment.i18n.lang switch
                {
                    "ru" => Language.Russian,
                    "en" => Language.English,
                    "tr" => Language.Turkish,
                    _ => Language.English
                };

                var newPrefsRouter = new PlayerPrefsRouter();

                newPrefsRouter.SetLanguage(lang);

                newPrefsRouter.SetToPrefs();
            }

            IsInited.Value = true;
        }
    }
}