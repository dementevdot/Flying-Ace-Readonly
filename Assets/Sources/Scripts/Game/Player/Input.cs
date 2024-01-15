using System;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class Input : MonoBehaviour
    {
        [SerializeField] private Transform _movableTransform;

        private GameObject _inputPlane;
        private RaycastHit[] _hits;
        private Vector3 _pointRotation;
        private Camera _mainCamera;

        public ReactiveProperty<Vector3> HitPosition { get; } = new ();

        public void Init(Camera mainCamera, GameObject inputPlane)
        {
            _mainCamera = mainCamera;
            _inputPlane = inputPlane;

            Transform cameraTransform = _mainCamera.transform;
            Vector3 direction = Quaternion.AngleAxis(90, Vector3.right) * cameraTransform.forward;

            Ray rayCam = new Ray(cameraTransform.position, direction);

            if (Physics.Raycast(rayCam, out var hit))
                _pointRotation = new Vector3(0, 0, hit.point.z);
            else
                throw new InvalidOperationException();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0) == false) return;

            Ray ray = _mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            _hits = Physics.RaycastAll(ray, 50f);

            foreach (var raycastHit in _hits)
            {
                if (raycastHit.collider.gameObject.layer != _inputPlane.layer) continue;

                var hitPoint = raycastHit.point;
                var clampedPosition = new Vector3(hitPoint.x, hitPoint.y, Mathf.Clamp(hitPoint.z, -5, 30));

                HitPosition.Value = clampedPosition;
            }

            Vector3 direction = _pointRotation - _movableTransform.position;
            _movableTransform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}
