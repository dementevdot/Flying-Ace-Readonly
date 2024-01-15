using Data.SO;
using Shared;
using UnityEngine;

namespace UI.LevelPicker
{
    public class CellFactory : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private Levels _levels;
        [SerializeField] private Leaderboard.View _leaderboard;

        private void Awake()
        {
            for (int i = 0; i < _levels.LevelsCount; i++)
                Instantiate(_cellPrefab, _parentTransform).Init((uint)i, _levelLoader, _leaderboard);
        }
    }
}