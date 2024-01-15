using System.Linq;
using UnityEngine;

namespace Data.SO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/New Skins", fileName = "Skins")]
    public class Skins : ScriptableObject
    {
        [SerializeField] private Skin[] _skinsPrefabs;

        public GameObject GetSkinPrefab(SkinName skinName) => _skinsPrefabs.First(skin => skin.SkinName == skinName).Prefab;
    }
}
