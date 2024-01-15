using System.Collections.Generic;
using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Leaderboard
{
    public class View : MonoBehaviour
    {
        private readonly List<Cell> _cellPool = new ();

        [SerializeField] private Transform _parentTransform;
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private GameObject _authPanel;
        [SerializeField] private Button _authButton;
        [SerializeField] private TMP_Text _label;

        private LeaderboardEntryResponse[] _entries;

        private void OnEnable()
        {
            bool isAuthorized = PlayerAccount.IsAuthorized;

            if (YandexGamesSdk.IsRunningOnYandex && isAuthorized)
            {
                UpdateLeaderboard();
                return;
            }

            if (isAuthorized == false)
            {
                _authButton.onClick.AddListener(Auth);
                return;
            }

            SetDebugLeaderboard();
        }

        public void Init(LeaderboardGetEntriesResponse response, string label = "")
        {
            if (response != null)
                _entries = response.entries;

            _label.text = label;

            OnEnable();
        }

        private void UpdateLeaderboard()
        {
            _authPanel.SetActive(false);

            if (_entries == null) return;

            if (_entries.Length != _cellPool.Count)
            {
                var difference = _entries.Length - _cellPool.Count;

                if (difference > 0)
                {
                    for (int i = 0; i < difference; i++)
                    {
                        var cell = Instantiate(_cellPrefab, _parentTransform);
                        cell.Init(i, i.ToString(), i);
                        _cellPool.Add(cell);
                    }
                }
                else
                {
                    difference = -difference;

                    for (int i = 0; i < difference; i++)
                    {
                        var lastIndex = _cellPool.Count - 1;

                        Destroy(_cellPool[lastIndex].gameObject);
                        _cellPool.RemoveAt(lastIndex);
                    }
                }
            }

            for (int i = 0; i < _entries.Length; i++)
            {
                _cellPool[i].Init(_entries[i].rank, _entries[i].player.publicName, _entries[i].score);
            }
        }

        private void SetDebugLeaderboard()
        {
            _authPanel.SetActive(false);

            var cellsCount = 5;

            if (cellsCount != _cellPool.Count)
            {
                var difference = cellsCount - _cellPool.Count;

                if (difference > 0)
                {
                    for (int i = 0; i < difference; i++)
                    {
                        var cell = Instantiate(_cellPrefab, _parentTransform);
                        cell.Init(i, i.ToString(), i);
                        _cellPool.Add(cell);
                    }
                }
                else
                {
                    difference = -difference;

                    for (int i = 0; i < difference; i++)
                    {
                        var lastIndex = _cellPool.Count - 1;
                        Destroy(_cellPool[lastIndex].gameObject);
                        _cellPool.RemoveAt(lastIndex);
                    }
                }
            }

            for (int i = 0; i < cellsCount; i++)
                _cellPool[i].Init(i, i.ToString(), i);
        }

        private void Auth()
        {
            if (PlayerAccount.IsAuthorized == true)
                UpdateLeaderboard();
            else
                PlayerAccount.Authorize();
        }
    }
}
