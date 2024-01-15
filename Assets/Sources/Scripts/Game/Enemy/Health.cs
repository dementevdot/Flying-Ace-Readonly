using System;
using Game.Shared;
using UniRx;
using UnityEngine;

namespace Game.Enemy
{
    public sealed class Health : Shared.Health
    {
        [SerializeField] private CombatCollider _combatCollider;
        [SerializeField] private uint _maxHealth = 1;

        public static event Action BossDied;

        private void OnDestroy()
        {
            _combatCollider.OnDamage -= TakeDamage;
        }

        public void Init(bool isBoss)
        {
            IsDead.Subscribe(isDead => { if (isDead == true) Die(); }).AddTo(_disposable);

            if (isBoss == true)
            {
                IsDead.Subscribe(isDead =>
                {
                    if (isDead == true)
                    {
                        BossDied?.Invoke();
                    }
                }).AddTo(_disposable);
            }

            _combatCollider.OnDamage += TakeDamage;
        }

        protected override uint GetInitialHealth() => _maxHealth;

        private void Die() => Destroy(transform.parent.gameObject);
    }
}
