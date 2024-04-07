using System;
using Game;
using SmallShips;
using UnityEngine;
using VContainer;

namespace ShipSystem
{
    public class Player : MonoBehaviour
    {
        [Inject] private ShipController _shipController;
        
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Animator _animator;
        [SerializeField] private ExplosionController _explosion;
        
        public Action OnСollision;

        public RectTransform Rect => _rect;
        
        [Inject]
        public void Construct()
        {
            _shipController.InitPlayer(this);
        }

        public void PlayAnimation(string id) => _animator.Play(id);
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.tag.Equals(Constants.AsteroidTag))return;
            
            OnСollision?.Invoke();
        }

        public void RestartGame()
        {
            _explosion.Restart();
        }
    }
}