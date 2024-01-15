using System.Collections;
using UnityEngine;

namespace Game.Bullet
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Transform _transformToMove;
        [SerializeField] private float _movementSpeed = 25;

        private Health _health;
        private bool _targetReached;

        public void Init(Health health)
        {
            _health = health;
        }

        public void StartMoving(Vector3 targetPosition)
        {
            StartCoroutine(StartStraightMove(targetPosition));
        }

        private IEnumerator StartStraightMove(Vector3 targetPosition)
        {
            _targetReached = false;

            _transformToMove.LookAt(targetPosition);

            while (_targetReached == false)
            {
                var nextPosition = Vector3.MoveTowards(
                    _transformToMove.position,
                    targetPosition,
                    _movementSpeed * Time.deltaTime);

                _transformToMove.position = nextPosition;

                if (nextPosition == targetPosition)
                {
                    _targetReached = true;
                    continue;
                }

                yield return new WaitForEndOfFrame();
            }

            _health.Die();
        }
    }
}
