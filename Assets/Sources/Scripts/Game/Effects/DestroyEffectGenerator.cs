using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Effects
{
    public class DestroyEffectGenerator : MonoBehaviour
    {
        private const int DefaultCount = 3;
        private readonly List<GameObject> _effects = new ();

        [SerializeField] private ParticleSystem _particleExplosion;

        private void Awake()
        {
            for (int i = 0; i < DefaultCount; i++)
                Create();
        }

        public GameObject GetFreeEffect()
        {
            var effect = _effects.FirstOrDefault(effect => effect.activeInHierarchy == false);

            return effect == null ? Create() : effect;
        }

        private GameObject Create()
        {
            GameObject effect = Instantiate(_particleExplosion.gameObject, transform);
            _effects.Add(effect);

            return effect;
        }
    }
}
