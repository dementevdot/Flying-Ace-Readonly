using System;
using UnityEngine;

namespace Data.Serializables
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private uint _levelIndex;
        [SerializeField] private uint _score;

        public LevelData(uint levelIndex, uint score)
        {
            _levelIndex = levelIndex;
            _score = score;
        }

        public uint LevelIndex => _levelIndex;
        public uint Score => _score;

        public void UpdateValues(uint score)
        {
            if (score > _score)
                _score = score;
        }
    }
}