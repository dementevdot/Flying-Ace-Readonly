using Data.SO;
using UnityEngine;

namespace Game.Player
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private Skins _skins;

        private void Awake()
        {
            Instantiate(_skins.GetSkinPrefab(_data.CurrentSkin.Value), transform.parent);
        }
    }
}
