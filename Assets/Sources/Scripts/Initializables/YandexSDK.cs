using System.Collections;
using Agava.YandexGames;

namespace Initializables
{
    public class YandexSDK : Base
    {
        private IEnumerator Start()
        {
            if (YandexGamesSdk.IsRunningOnYandex == true)
            {
                yield return YandexGamesSdk.Initialize(() =>
                {
                    InterstitialAd.Show();
                    IsInited.Value = true;
                });
                yield break;
            }

            IsInited.Value = true;
            yield return null;
        }
    }
}