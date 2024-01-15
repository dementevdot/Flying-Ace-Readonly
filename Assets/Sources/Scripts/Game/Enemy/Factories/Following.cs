using UnityEngine;

namespace Game.Enemy.Factories
{
    public class Following : Base
    {
        [SerializeField] private Transform _followed;
        [SerializeField] private Vector3 _offset;

        protected override void Create()
        {
            InstantiateEnemy(_transform.position, _transform.parent).StartMove(_ => _followed.position + _offset);
        }
    }
}
