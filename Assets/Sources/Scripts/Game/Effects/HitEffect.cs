using System.Collections;
using System.Collections.Generic;
using Game.Shared;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Effects
{
    public class HitEffect : MonoBehaviour
    {
        private const string Emission = "_EMISSION";
        private const string EmissionColor = "_EmissionColor";

        private readonly List<Material> _materials = new ();

        [SerializeField] private Color _blinkColor = Color.white;
        [SerializeField] private float _hitEffectDuration = 0.05f;
        [FormerlySerializedAs("_root")] [SerializeField] private GameObject _meshParent;
        [SerializeField] private AudioClip _clip;

        private IHealth _health;
        private MeshRenderer[] _meshRenderers;

        private void Start()
        {
            _meshRenderers = _meshParent.GetComponentsInChildren<MeshRenderer>();

            foreach (var meshRenderer in _meshRenderers)
                _materials.AddRange(meshRenderer.materials);
        }

        private void OnEnable()
        {
            _health = GetComponent<IHealth>();
            _health.OnDamage += OnHit;
        }

        private void OnDisable()
        {
            _health.OnDamage -= OnHit;
        }

        private void OnHit()
        {
            foreach (var material in _materials)
            {
                material.EnableKeyword(Emission);
                material.SetColor(Shader.PropertyToID(EmissionColor), _blinkColor);
                AudioSource.PlayClipAtPoint(_clip, transform.position);
            }

            StartCoroutine(ResetMaterial());
        }

        private IEnumerator ResetMaterial()
        {
            yield return new WaitForSeconds(_hitEffectDuration);

            foreach (var material in _materials)
                material.SetColor(Shader.PropertyToID(EmissionColor), Color.black);
        }
    }
}
