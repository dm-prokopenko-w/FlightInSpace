using System;
using Game;
using UISystem;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;
using Application = UnityEngine.Device.Application;

namespace GameplaySystem
{
    public class GameplayController: IStartable
    {
        [Inject] private UIController _uiController;

        public Action<bool> OsPlayGame;
        public Action OnGameOver;
        public Action OnResetGame;

        private bool _isPlayGame = false;
        private int _countAsteroidCollision = 0;
        
        public void Start()
        {
            _uiController.SetAction(Constants.GameBtnID, UpdateGame);
            _uiController.SetAction(Constants.ResetBtnID, ResetGame);
            _uiController.SetAction(Constants.QuitBtnID, Application.Quit);
            _uiController.SetText(Constants.AsteroidCollisionCounterID, "0");
        }

        private void UpdateGame()
        {
            _isPlayGame = !_isPlayGame;
            OsPlayGame?.Invoke(_isPlayGame);
        }

        private void ResetGame()
        {
            _countAsteroidCollision = 0;
            _uiController.SetText(Constants.AsteroidCollisionCounterID, _countAsteroidCollision.ToString());
            OnResetGame?.Invoke();
        }
        
        public void GameOver()
        {
            _countAsteroidCollision++;
            _uiController.SetText(Constants.AsteroidCollisionCounterID, _countAsteroidCollision.ToString());
            OnGameOver?.Invoke();
        }
    }
}
