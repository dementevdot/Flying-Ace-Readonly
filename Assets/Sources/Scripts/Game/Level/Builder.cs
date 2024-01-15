using UI;
using UnityEngine;

namespace Game.Level
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _inputPlane;
        [SerializeField] private CompleteHandler _completeHandler;
        [SerializeField] private DeadHandler _deadHandler;

        private Camera _camera;

        public Player.Main Player { get; private set; }

        public void Init(Data.SO.Level level, uint levelIndex, bool endless, Camera camera, ScreenHandler screenHandler)
        {
            _camera = camera;

            SpawnPlayer();

            level.Instantiate(transform, _camera, endless);

            _completeHandler.Init(screenHandler, levelIndex, level);
            _deadHandler.Init(screenHandler, endless, level);
        }

        public void SpawnPlayer(uint? health = null, uint? points = null)
        {
            Player = Instantiate(_playerPrefab, transform).GetComponentInChildren<Player.Main>();

            Player.Init(_camera, _inputPlane);

            if (health != null)
                Player.Health.SetHealth((uint)health);

            if (points != null)
                Player.Points.SetPoints((uint)points);
        }
    }
}
