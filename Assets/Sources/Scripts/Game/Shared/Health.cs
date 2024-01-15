using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Shared
{
	public abstract class Health : MonoBehaviour, IHealth
	{
		protected readonly CompositeDisposable _disposable = new();
		private readonly ReactiveProperty<uint> _currentHealth = new();

		public event Action OnDamage;

		public ReadOnlyReactiveProperty<uint> CurrentHealth { get; private set; }

		public BoolReactiveProperty IsDead { get; } = new();

		private void Awake()
		{
			_currentHealth.Value = GetInitialHealth();
			CurrentHealth = new ReadOnlyReactiveProperty<uint>(_currentHealth);

			_currentHealth.Subscribe(health =>
			{
				if (health == 0) IsDead.Value = true;
			}).AddTo(_disposable);

			this.OnDestroyAsObservable().Subscribe(_ => { _disposable.Clear(); }).AddTo(_disposable);
		}

		public void SetHealth(uint health) => _currentHealth.Value = health;

		protected abstract uint GetInitialHealth();

		protected void TakeDamage(uint damage)
		{
			if (damage >= _currentHealth.Value)
				_currentHealth.Value = 0;
			else
				_currentHealth.Value -= damage;

			OnDamage?.Invoke();
		}
	}
}