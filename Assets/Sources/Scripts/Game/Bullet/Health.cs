using Game.Shared;
using UnityEngine;

namespace Game.Bullet
{
    public class Health : MonoBehaviour
    {
        private CombatCollider _combatCollider;

        private void OnDestroy() => _combatCollider.OnDamage -= OnCombatColliderDamage;

        public void Init(CombatCollider combatCollider)
        {
            _combatCollider = combatCollider;

            _combatCollider.OnDamage += OnCombatColliderDamage;
        }
        
        public void Die() => Destroy(gameObject);

        private void OnCombatColliderDamage(uint damage) => Die();
    }
}
