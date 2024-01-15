using System.Collections;
using UnityEngine;

namespace Game.Bullet.Factories
{
    public class Triple : Base
    {
        private const int CreateCount = 3;

        [SerializeField] private float _angle = 45;

        protected override IEnumerator Shooting()
        {
            var delay = new WaitForSecondsRealtime(_shootingDelay);

            while (true)
            {
                yield return delay;

                Vector3 eulerDirection = _movableTransform.rotation.eulerAngles;
                Vector3 rotationAngle = new (0, _angle, 0);

                eulerDirection -= rotationAngle;

                for (int i = 0; i < CreateCount; i++)
                {
                    CreateBullet(_movableTransform.position, Quaternion.Euler(eulerDirection), FlyDuration);

                    eulerDirection += rotationAngle;
                }
            }
        }
    }
}
