using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class Movement : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        private const uint MoveHeight = 0;

        [SerializeField] private float _maxMovementSpeed = 25;
        [SerializeField] private float _speedSmoothing = 5;
        [SerializeField] private Transform _transformToMove;

        private Input _input;
        private Vector3 _targetPosition;

        public void Init(Input input)
        {
            _input = input;

            _input.HitPosition.
                Subscribe(position => _targetPosition = new Vector3(position.x, MoveHeight, position.z))
                .AddTo(_disposable);
        }

        private void OnDestroy() => _disposable.Clear();

        private void Update() => Move();

        private void Move()
        {
            if (_transformToMove.position == _targetPosition) return;

            Vector3 currentPosition = _transformToMove.position;
            float movmentSpeed = _maxMovementSpeed * Mathf.Clamp01(Vector3.Distance(currentPosition, _targetPosition) / _speedSmoothing);

            Vector3 newPosition = Vector3.MoveTowards(
                currentPosition,
                _targetPosition,
                movmentSpeed * Time.deltaTime);

            _transformToMove.position = newPosition;
        }
    }
}
