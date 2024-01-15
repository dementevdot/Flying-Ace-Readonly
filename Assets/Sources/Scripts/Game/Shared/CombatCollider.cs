using System;
using UnityEngine;

namespace Game.Shared
{
    public class CombatCollider : MonoBehaviour
    {
        [SerializeField] private ColliderOwner _colliderOwner;

        public ColliderOwner ColliderOwner => _colliderOwner;

        public event Action<CombatCollider> OnEnter;

        public event Action<uint> OnDamage;

        private void OnTriggerEnter(Collider enemyCollider)
        { 
            if (enemyCollider.TryGetComponent<CombatCollider>(out var combatCollider) == true) 
                OnEnter?.Invoke(combatCollider);
        }

        public void TakeDamage(uint damage) => OnDamage?.Invoke(damage);
    }
}
