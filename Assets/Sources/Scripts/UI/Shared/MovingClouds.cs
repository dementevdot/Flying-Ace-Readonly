using UnityEngine;

namespace UI.Shared
{
    public class MovingClouds : MonoBehaviour
    {
        [SerializeField] private Material _materialCloud;
        [SerializeField] private float _scrollSpeed;

        private int _textureIndex;

        private void Awake()
        {
            _textureIndex = Shader.PropertyToID("_MainTex");
        }

        private void OnDisable()
        {
            _materialCloud.SetTextureOffset(_textureIndex, new Vector2(0, 0));
        }

        private void FixedUpdate() => Move();

        private void Move()
        {
            float offset = Time.time * _scrollSpeed;
            _materialCloud.SetTextureOffset(_textureIndex, new Vector2(0.01f, offset));
        }
    }
}
