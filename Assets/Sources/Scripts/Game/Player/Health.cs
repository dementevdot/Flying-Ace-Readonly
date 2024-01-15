using Game.Shared;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public sealed class Health : Shared.Health
    {
        [SerializeField] private CombatCollider _combatCollider;
        [SerializeField] private uint _maxHealth = 100;

        public void Init()
        {
            IsDead.Subscribe(isDead =>
            {
                if (isDead == true) Destroy(transform.parent.gameObject);
            }).AddTo(_disposable);

            _combatCollider.OnDamage += TakeDamage;
        }

        private void OnDestroy()
        {
            _combatCollider.OnDamage -= TakeDamage;
        }

        public void ApplyAdViewHealth()
        {
            SetHealth(CurrentHealth.Value + Constants.HealthForAdView);
        }

        protected override uint GetInitialHealth() => _maxHealth;
    }
}
