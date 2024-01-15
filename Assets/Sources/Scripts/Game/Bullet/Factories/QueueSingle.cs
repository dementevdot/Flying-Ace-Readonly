using System.Collections;
using UnityEngine;

namespace Game.Bullet.Factories
{
    public class QueueSingle : Base
    {
        [SerializeField] private float _queueDelay = 0.1f;
        [SerializeField] private int _createCount = 8;

        protected override IEnumerator Shooting()
        {
            var shootingDelay = new WaitForSecondsRealtime(_shootingDelay);
            var queueDelay = new WaitForSecondsRealtime(_queueDelay);

            while (true)
            {
                yield return queueDelay;

                for (int i = 0; i < _createCount; i++)
                {
                    yield return shootingDelay;

                    CreateBullet(_movableTransform.position, _movableTransform.rotation, FlyDuration);
                }
            }
        }
    }
}
