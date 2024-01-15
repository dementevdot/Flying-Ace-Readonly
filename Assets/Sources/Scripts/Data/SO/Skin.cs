using UnityEngine;

namespace Data.SO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/New Skin", fileName = "Skin")]
    public class Skin : ScriptableObject
    {
        [SerializeField] private SkinName _skinName;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private uint _price;

        public SkinName SkinName => _skinName;
        public GameObject Prefab => _prefab;
        public uint Price => _price;
    }
}
