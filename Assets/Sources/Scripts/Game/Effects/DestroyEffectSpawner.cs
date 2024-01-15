using UnityEngine;

namespace Game.Effects
{
    [RequireComponent(typeof(DestroyEffectGenerator))]
    public class DestroyEffectSpawner : MonoBehaviour
    {
        private DestroyEffectGenerator _generator;

        private void Awake()
        {
            _generator = GetComponent<DestroyEffectGenerator>();
        }

        private void OnEnable()
        {
            DestroyEffect.OnEffectActivated += OnDestroyEffect;
        }

        private void OnDisable()
        {
            DestroyEffect.OnEffectActivated -= OnDestroyEffect;
        }

        private void OnDestroyEffect(Vector3 effectPosition)
        {
            GameObject effect = _generator.GetFreeEffect();
            effect.transform.position = effectPosition;
            effect.SetActive(true);
        }
    }
}
