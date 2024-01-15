using System.Collections.Generic;
using UnityEngine;

namespace Game.Ground
{
    public class Ground : ObjectPool
    {
        private readonly List<GameObject> _sections = new ();

        [SerializeField] private GameObject[] _sectionPrefabs;
        [SerializeField] private int _generateSectionCount;
        [SerializeField] private float _speed;

        private Vector3 _cameraPosition;
        private float _distanceBetweenSection;
        private int _currentSection = 0;

        private void Awake()
        {
            Vector3 size = _sectionPrefabs[0].GetComponent<MeshRenderer>().bounds.size;
            _distanceBetweenSection = size.z;

            foreach (var section in _sectionPrefabs)
                Initialize(section);

            for (int i = 0; i < _generateSectionCount; i++)
                ActivateSection();
        }

        private void FixedUpdate()
        {
            Move();

            if (_currentSection != _generateSectionCount)
                ActivateSection();

            DeactivateSection();
        }

        private void OnValidate()
        {
            _generateSectionCount = Mathf.Clamp(_generateSectionCount, 1, _sectionPrefabs.Length - 1);
        }

        public void Init(Camera mainCamera)
        {
            _cameraPosition = mainCamera.transform.position;
        }

        private void Move()
        {
            foreach (GameObject section in _sections)
            {
                section.transform.Translate(Vector3.back * (_speed * Time.deltaTime));
            }
        }

        private void SetSection(GameObject section, float spawnPointZ)
        {
            section.SetActive(true);
            var position = section.transform.position;
            position = new Vector3(position.x, position.y, spawnPointZ);
            section.transform.position = position;
            _sections.Add(section);
            _currentSection++;
        }

        private void ActivateSection()
        {
            if (TryGetFirstObject(out GameObject section))
            {
                SetSection(section, _sections.Count > 0 ? _sections[_sections.Count - 1].transform.position.z + _distanceBetweenSection : section.transform.position.z);
            }
        }

        private void DeactivateSection()
        {
            if (_sections[0].transform.localPosition.z + _distanceBetweenSection < _cameraPosition.z == false) return;

            _sections[0].SetActive(false);
            _sections.RemoveAt(0);
            _currentSection--;
        }
    }
}
