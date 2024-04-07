using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Core;
using GameplaySystem;
using UISystem;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace AsteroidsSystem
{
    public class AsteroidsController : IStartable, IDisposable, ITickable
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private AssetLoader _assetLoader;
        [Inject] private UIController _uiController;

        private ObjectPool<Asteroid> _pool;

        private bool _isEnd = false;
        private UnityEvent OnTick = new ();

        private int _secondsBeforeSpawnMin;
        private int _secondsBeforeSpawnMax;
        
        private float _asteroidSpeedMin;
        private float _asteroidSpeedMax;
        
        private bool _isPlay;
        private List<Asteroid> _activeAsteroids = new();
        
        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
            _gameplay.OnGameOver += DespawnAll;
            _gameplay.OnResetGame += DespawnAll;

            var data = _assetLoader.LoadConfig(Constants.AsteroidsConfigPath) as AsteroidsConfig;

            _secondsBeforeSpawnMin = data.SecondsBeforeSpawnMin * 1000;
            _secondsBeforeSpawnMax = data.SecondsBeforeSpawnMax * 1000;

            _asteroidSpeedMin = data.AsteroidSpeedMin / 2;
            _asteroidSpeedMax = data.AsteroidSpeedMax / 2;
            
            var activeTr =
                _uiController.GetTransformParent(Constants.ParentAsteroids + Constants.AsteroidsParentType.Active);
            var inactiveTr =
                _uiController.GetTransformParent(Constants.ParentAsteroids + Constants.AsteroidsParentType.Inactive);
            
            _pool = new ObjectPool<Asteroid>();
            _pool.InitPool(data.AsteroidPrefab, inactiveTr);
            SpawnAsteroids(data.AsteroidPrefab, activeTr);
        }

        private async void SpawnAsteroids(Asteroid prefab, Transform parent)
        {
            var posY = Screen.height + 300;
            var startPosY = Camera.main.ScreenToWorldPoint(new Vector2(0, posY)).y;
            var endPosY = Camera.main.ScreenToWorldPoint(new Vector2(0, - 300f)).y;

            var minPosX = Camera.main.ScreenToWorldPoint(new Vector2(0, posY)).x;
            var maxPosX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, posY)).x;

            while (true)
            {
                var second = Random.Range(_secondsBeforeSpawnMin, _secondsBeforeSpawnMax);
                await Task.Delay(second);

                if (_isEnd) break;

                if (!_isPlay) continue;
                
                var posX = Random.Range(minPosX, maxPosX);

                var asteroid = _pool.Spawn(prefab, new Vector2(posX, startPosY), Quaternion.identity, parent);
                
                var speed = Random.Range(_asteroidSpeedMin, _asteroidSpeedMax);
                asteroid.Init(endPosY, OnTick, Despawn, speed);
                _activeAsteroids.Add(asteroid);
            }
        }

        private void DespawnAll()
        {
            foreach (var asteroid in _activeAsteroids)
            {
                _pool.Despawn(asteroid);
            }
            _activeAsteroids.Clear();
        }
        
        private void Despawn(Asteroid asteroid)
        {
            _activeAsteroids.Remove(asteroid);
            _pool.Despawn(asteroid);
        }

        public void Dispose()
        {
            _gameplay.OnGameOver -= DespawnAll;
            _gameplay.OnResetGame -= DespawnAll;
            _gameplay.OsPlayGame -= (value) => _isPlay = value;
            _isEnd = true;
        }

        public void Tick()
        {
            if (!_isPlay) return;

            OnTick?.Invoke();
        }
    }
}