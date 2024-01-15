using Agava.YandexGames;
using Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Start
{
    public class LeaderboardButton : MonoBehaviour
    {
        [SerializeField] private Leaderboard.View _leaderboard;
        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private TextContainer _labels;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                string label = _data.Language.Value switch
                {
                    Language.Russian => _labels.Russian,
                    Language.English => _labels.English,
                    Language.Turkish => _labels.Turkish,
                    _ => string.Empty
                };

                if (YandexGamesSdk.IsRunningOnYandex == true && PlayerAccount.IsAuthorized == true)
                {
                    Agava.YandexGames.Leaderboard.GetEntries(Constants.MainLeaderboard, response =>
                    {
                        _leaderboard.Init(response, label);
                    });
                }
                else
                {
                    _leaderboard.Init(null, label);
                }
            });
        }
    }
}