using Game.Shared;
using UnityEngine;

namespace Game.Player
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private CombatCollider _combatCollider;
        [SerializeField] private uint _damage = 1;
        [SerializeField] private Bullet.Factories.Base _bulletFactory;
        [SerializeField] private Data.SO.Game _data;

        private Coroutine _shootingCoroutine;

        public void Init()
        {
            _bulletFactory.SetShootingDelay(_data.ShootDelay);

            _combatCollider.OnEnter += OnCombatColliderEnter;
        }

        private void OnCombatColliderEnter(CombatCollider combatCollider)
        {
            if (combatCollider.ColliderOwner != ColliderOwner.Enemy) return;

            combatCollider.TakeDamage(_damage);
            _combatCollider.TakeDamage(Constants.EnemyDamage);
        }
    }
}
