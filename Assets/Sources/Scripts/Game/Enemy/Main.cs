using System;
using UniRx;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    public class Main : MonoBehaviour
    {
        [SerializeField] private bool _isBoss;
        [SerializeField] private uint _rewardPoins = 1;

        private Health _health;
        private Movement _movement;

        public static event Action<Main> OnEnemyDied;

        public uint RewardPoints => _rewardPoins;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _movement = GetComponent<Movement>();

            _movement.Init(_isBoss);
            _health.Init(_isBoss);

            _health.IsDead.Subscribe(isDead =>
            {
                if (isDead == true)
                {
                    OnEnemyDied?.Invoke(this);
                }
            });
        }
    }
}