using System.Collections;
using Game.Shared;
using UnityEngine;

namespace Game.Bullet.Factories
{
    public abstract class Base : MonoBehaviour
    {
        [SerializeField] protected Transform _movableTransform;
        [SerializeField] protected Transform _rootTransform;
        [SerializeField] protected Bullet.Main _bulletPrefab;
        [SerializeField] protected ColliderOwner _colliderToDamage;
        [SerializeField] protected float _shootingDelay;
        [SerializeField] protected float _maxBulletZ_Position = -5;

        private Coroutine _shootingCoroutine;

        protected float FlyDuration => _maxBulletZ_Position < 0 ? 
                _movableTransform.position.z - _maxBulletZ_Position : _maxBulletZ_Position - _movableTransform.position.z;

        private void OnEnable()
        {
            _shootingCoroutine = StartCoroutine(Shooting());
        }

        private void OnDisable()
        {
            if (_shootingCoroutine != null)
                StopCoroutine(_shootingCoroutine);
        }

        public void SetShootingDelay(float delay) => _shootingDelay = delay;

        protected abstract IEnumerator Shooting();

        protected void CreateBullet(Vector3 position, Quaternion direction, float flyDuration)
        {
            var bullet = Instantiate(_bulletPrefab, _movableTransform.position, Quaternion.identity, _rootTransform.parent);
            bullet.Init(_colliderToDamage);
            bullet.Movement.StartMoving(position + direction * Vector3.forward * flyDuration);
        }
    }
}
