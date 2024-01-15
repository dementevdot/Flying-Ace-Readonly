using System;
using Agava.YandexGames;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private Data.SO.Game _data;
    [SerializeField] private Text _loadingText;
    [SerializeField] private Initializables.Base[] _initializables;

    private uint _initedCount;

    public event Action Inited;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Inited += () =>
        {
            _data.Init();
            var loadAsync = SceneManager.LoadSceneAsync("Game");
            loadAsync.completed += _ => YandexGamesSdk.GameReady();
        };

        foreach (var initializable in _initializables)
        {
            initializable.IsInited.Subscribe(isInited =>
            {
                if (isInited == false) return;

                _initedCount++;
                _loadingText.text += ".";

                if (_initedCount == _initializables.Length)
                    Inited?.Invoke();
            });
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerAccount.GetCloudSaveData(Debug.Log);
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                PlayerPrefs.DeleteAll();
                PlayerAccount.SetCloudSaveData("{}");
            }
        }
    }
}
