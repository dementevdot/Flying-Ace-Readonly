using System;
using UniRx;

namespace Game.Shared
{
    public interface IHealth
    {
        public event Action OnDamage;

        public ReadOnlyReactiveProperty<uint> CurrentHealth { get; }
        public BoolReactiveProperty IsDead { get; }
    }
}