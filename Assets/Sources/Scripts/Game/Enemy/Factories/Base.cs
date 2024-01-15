using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy.Factories
{
    public abstract class Base : MonoBehaviour
    {
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector2 _spawnDelayRange = new (1, 2);
        [SerializeField] protected int _countCreate;

        protected Transform _transform;

        private int _tempValue;

        protected virtual void Awake()
        {
            _transform = transform;
            _tempValue = _countCreate;
        }

        private void OnEnable()
        {
            StartCoroutine(Spawn());
        }

        protected abstract void Create();

        protected Enemy.Movement InstantiateEnemy(Vector3 position, Transform parent)
        {
            return Instantiate(_prefab, position, Quaternion.identity, parent).GetComponentInChildren<Enemy.Movement>();
        }

        private IEnumerator Spawn()
        {
            while (_countCreate > 0)
            {
                yield return new WaitForSecondsRealtime(Random.Range(_spawnDelayRange.x, _spawnDelayRange.y));
                Create();
                _countCreate--;
            }

            _countCreate = _tempValue;
        }
    }
}