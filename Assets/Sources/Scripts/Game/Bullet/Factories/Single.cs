using System.Collections;
using UnityEngine;

namespace Game.Bullet.Factories
{
    public class Single : Base
    {
        protected override IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(_shootingDelay);

                CreateBullet(_movableTransform.position, _movableTransform.rotation, FlyDuration);
            }
        }
    }
}
