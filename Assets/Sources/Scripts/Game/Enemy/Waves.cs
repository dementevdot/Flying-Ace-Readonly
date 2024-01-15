using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class Waves : MonoBehaviour
    {
        [SerializeField] private Wave[] _waveEnemies;
        [SerializeField] private Wave _bossWave;
        [SerializeField] private float _delay = 10f;

        private bool _endless = false;

        public void Init(bool endless) => _endless = endless;

        private void Start()
        {
            StartCoroutine(ActivateSequentialRoutine());
        }

        private IEnumerator ActivateSequentialRoutine()
        {
            int index = 0;

            while (index != _waveEnemies.Length)
            {
                if (index < _waveEnemies.Length)
                {
                    foreach (var wave in _waveEnemies[index].EnemyFactories)
                    {
                        wave.SetActive(true);
                    }

                    index++;
                }

                yield return new WaitForSeconds(_delay);

                foreach (var wave in _waveEnemies[index - 1].EnemyFactories)
                {
                    wave.SetActive(false);
                }

                if (index == _waveEnemies.Length && _endless == true)
                {
                    index = 0;
                }
            }

            foreach (var wave in _bossWave.EnemyFactories)
            {
                wave.SetActive(true);
            }
        }
    }
}