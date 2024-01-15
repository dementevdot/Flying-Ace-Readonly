using Game.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        private IHealth _health;

        private void OnEnable()
        {
            _health = GetComponent<IHealth>();
            _health.OnDamage += OnFill;
        }

        private void OnDisable()
        {
            _health.OnDamage -= OnFill;
        }

        private void OnFill()
        {
            _fill.fillAmount = _health.CurrentHealth.Value / 100f;
        }
    }
}
