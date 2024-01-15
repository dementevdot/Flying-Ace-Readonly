using Game.Shared;
using UnityEngine;

namespace Game.Bullet
{
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(CombatCollider))]
    public class Main : MonoBehaviour
    {
        [SerializeField] private Combat _combat;
        [SerializeField] private Health _health;
        [SerializeField] private Movement _movement;
        [SerializeField] private CombatCollider _combatCollider;

        public Movement Movement => _movement;

        public void Init(ColliderOwner colliderToDamage)
        {
            _combat.Init(_combatCollider, colliderToDamage, _health);
            _health.Init(_combatCollider);
            _movement.Init(_health);
        }
    }
}