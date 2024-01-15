using System;
using Game.Shared;
using UniRx;
using UnityEngine;

namespace Game.Effects
{
    public class DestroyEffect : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _health;

        public static event Action<Vector3> OnEffectActivated;

        private void Awake()
        {
            if (_health.IsInterface(typeof(IHealth)) == false) throw new InvalidOperationException(nameof(_health));

            var health = (IHealth)_health;

            health.IsDead.Subscribe(isDead => { if (isDead == true && health.CurrentHealth.Value == 0) Activate(); });
        }

        private void Activate() => OnEffectActivated?.Invoke(transform.position);
    }
}