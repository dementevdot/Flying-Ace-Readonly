using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Ground
{
    public class ObjectPool : MonoBehaviour
    {
        private const int FirstObject = 1;

        private readonly List<GameObject> _pool = new ();

        [SerializeField] private GameObject _container;

        protected void Initialize(GameObject prefab)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }

        protected bool TryGetRandomObject(out GameObject result)
        {
            var randomSelection = _pool.Where(x => x.activeSelf == false).Skip(FirstObject);
            var gameObjects = randomSelection.ToList();
            result = gameObjects.ElementAtOrDefault(new System.Random().Next() % gameObjects.Count());

            return result != null;
        }

        protected bool TryGetFirstObject(out GameObject result)
        {
            result = _pool.First(x => x.activeSelf == false);

            return result != null;
        }
    }
}
