using Game.Enemy;
using Game.Ground;
using Localization;
using UnityEngine;

namespace Data.SO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/New Level", fileName = "Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] private TextContainer _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private uint _difficult;
        [SerializeField] private Waves _waves;
        [SerializeField] private Ground _ground;
        [SerializeField] private string _leaderboardKey;
        [SerializeField] private uint _maxScore;

        public TextContainer Name => _name;
        public Sprite Sprite => _sprite;
        public uint Difficult => _difficult;
        public string LeaderboardKey => _leaderboardKey;

        public void Instantiate(Transform parent, Camera camera, bool endless)
        {
            Instantiate(_waves, parent).Init(endless);
            Instantiate(_ground, parent).Init(camera);
        }

        public uint GetStarsCount(uint score)
        {
            if (score == _maxScore) return 3;
            if (score > _maxScore / 2) return 2;
            if (score > _maxScore / 3) return 1;

            return 0;
        }
    }
}
