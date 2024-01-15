using UnityEngine;

namespace Game.Enemy.Factories
{
    public class Rotation : Base
    {
        [SerializeField] private Vector3 _target;

        private float _startDistance;

        protected override void Awake()
        {
            base.Awake();

            _startDistance = Vector3.Distance(_transform.position, _target);
        }

        protected override void Create()
        {
            InstantiateEnemy(_transform.position, _transform.parent).StartMove(position =>
            {
                float currentDistance = Vector3.Distance(position, _target);

                var transformPosition = _transform.position;

                return Vector3.Lerp(new Vector3(transformPosition.x, transformPosition.y, _target.z), _target, 1 - currentDistance / _startDistance);
            });
        }
    }
}
