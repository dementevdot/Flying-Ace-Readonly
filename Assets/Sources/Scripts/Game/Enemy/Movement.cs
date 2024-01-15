using System;
using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Transform _transformToMove;
        [SerializeField] private float _movementSpeed = 25;

        private bool _isBoss;

        private Vector3 TargetPosition { get; set; }

        public void StartMove(Func<Vector3, Vector3> directionGetter)
        {
            StartCoroutine(Move(directionGetter));
        }

        public void Init(bool isBoss) => _isBoss = isBoss;

        private IEnumerator Move(Func<Vector3, Vector3> directionGetter)
        {
            bool targetReached = false;

            while (targetReached == false)
            {
                var position = _transformToMove.position;

                TargetPosition = directionGetter.Invoke(position);

                if (position == TargetPosition)
                {
                    targetReached = true;
                    continue;
                }

                position = Vector3.MoveTowards(
                        position,
                        TargetPosition,
                        _movementSpeed * Time.deltaTime);

                _transformToMove.position = position;

                _transformToMove.LookAt(TargetPosition);

                yield return new WaitForEndOfFrame();
            }

            if (_isBoss == false)
                Destroy(transform.parent.gameObject);
        }
    }
}
