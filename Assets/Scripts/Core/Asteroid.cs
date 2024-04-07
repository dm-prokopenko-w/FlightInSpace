using System;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsSystem
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Transform _image;

        public bool IsMoved { get; set; }
        private UnityEvent _onTick = new ();

        private float _endPosY;
        private float _speed;
        private Action<Asteroid> _onDespawn;

        private void Start()
        {
            _onTick.AddListener(Move);
        }

        public void Init(float endPosY, UnityEvent onTick, Action<Asteroid> onDespawn, float speed)
        {
            _onTick = onTick;
            _endPosY = endPosY;
            IsMoved = true;
            _onDespawn = onDespawn;
            _speed = speed;
        }

        private void Move()
        {
            if(!IsMoved) return;
            
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
            _image.Rotate(0, 0, 1);

            if (!(_endPosY > transform.position.y)) return;
            IsMoved = false;
            _onDespawn?.Invoke(this);
        }
    }
}