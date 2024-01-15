using Game.Shared;
using UnityEngine;

namespace Game.Bullet
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private uint _damage = 1;

        private ColliderOwner _colliderToDamage;
        private CombatCollider _combatCollider;
        private Health _health;

        private void OnDestroy() => _combatCollider.OnEnter -= OnCombatColliderEnter;

        public void Init(CombatCollider combatCollider, ColliderOwner colliderToDamage, Health health)
        {
            _combatCollider = combatCollider;
            _colliderToDamage = colliderToDamage;
            _health = health;

            _combatCollider.OnEnter += OnCombatColliderEnter;
        }

        private void OnCombatColliderEnter(CombatCollider combatCollider)
        {
            if (_colliderToDamage == ColliderOwner.Player) return;

            if (combatCollider.ColliderOwner != _colliderToDamage) return;

            combatCollider.TakeDamage(_damage);
            _health.Die();
        }
    }
}
