using System;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(Input))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Points))]
    public class Main : MonoBehaviour
    {
        private Combat _combat;
        private Input _input;
        private Movement _movement;

        public static event Action<Health, Points> PlayerCreated;

        public Health Health { get; private set; }
        public Points Points { get; private set; }

        public void Init(Camera mainCamera, GameObject inputPlane)
        {
            _combat = GetComponent<Combat>();
            _input = GetComponent<Input>();
            _movement = GetComponent<Movement>();
            Health = GetComponent<Health>();
            Points = GetComponent<Points>();

            Health.Init();
            _combat.Init();
            _movement.Init(_input);
            _input.Init(mainCamera, inputPlane);

            PlayerCreated?.Invoke(Health, Points);
        }
    }
}
