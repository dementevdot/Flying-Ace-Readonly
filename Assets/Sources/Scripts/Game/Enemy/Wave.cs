using System;
using UnityEngine;

namespace Game.Enemy
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private GameObject[] _enemyFactories;

        public GameObject[] EnemyFactories => _enemyFactories;
    }
}