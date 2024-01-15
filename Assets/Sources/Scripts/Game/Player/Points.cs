using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class Points : MonoBehaviour
    {
        private readonly ReactiveProperty<uint> _currentPoints = new ();

        public ReadOnlyReactiveProperty<uint> CurrentPoints { get; private set; }

        private void Awake()
        {
            CurrentPoints = new ReadOnlyReactiveProperty<uint>(_currentPoints);
            Enemy.Main.OnEnemyDied += Collect;
        }

        private void OnDestroy()
        {
            Enemy.Main.OnEnemyDied -= Collect;
        }

        public void SetPoints(uint points) => _currentPoints.Value = points;

        private void Collect(Enemy.Main enemy) => _currentPoints.Value += enemy.RewardPoints;
    }
}