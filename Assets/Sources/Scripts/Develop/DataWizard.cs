using Data.Serializables;
using UnityEngine;

namespace Develop
{
    public class DataWizard : MonoBehaviour
    {
        [SerializeField] private PlayerPrefsRouter _prefs;
        [SerializeField] private Data.SO.Game _data;
        [SerializeField] private bool _getDataFromPlayerPrefs;

        private void Awake()
        {
            #if UNITY_EDITOR
            _prefs.SetToPrefs();
            _data.Init();
            #endif
        }

        private void OnValidate()
        {
            if (_getDataFromPlayerPrefs == true)
            {
                _getDataFromPlayerPrefs = false;
                _prefs.GetFromPrefs();
            }
        }
    }
}
