using UnityEngine;

namespace Data.SO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/New Levels", fileName = "Levels")]
    public class Levels : ScriptableObject
    {
        [SerializeField] private Level[] _levels;

        public int LevelsCount => _levels.Length;

        public Level GetLevel(uint index) => _levels[index];
    }
}