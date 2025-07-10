using Collectibles;
using Player;
using UI;
using UnityEngine;
using Utils;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private float gameDurationSecs = 60f;
        
        [Header("Setup")]
        [SerializeField] private PlayerController player;
        [SerializeField] private SpawnManager spawnManager;
        [SerializeField] private UIController uiController;
        [SerializeField] private HUDController hudController;
        [SerializeField] private GameInput gameInput;

        private bool _isGameActive;
        private float _remainingTime;
        private int _currentScore;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Awake()
        {
            EventSystem.OnCollectibleCollected += OnCollectibleCollected;
            _startPosition = player.transform.position;
            _startRotation = player.transform.rotation;
        }

        private void OnDestroy()
        {
            EventSystem.OnCollectibleCollected -= OnCollectibleCollected;
        }

        private void Update()
        {
            if (_isGameActive)
            {
                _remainingTime -= Time.deltaTime;

                if (_remainingTime <= 0f)
                {
                    EndGame();
                }
                else
                {
                    hudController.UpdateTimer(_remainingTime);
                }
            }
        }

        public bool GetIsGameActive()
        {
            return _isGameActive;
        }
        
        public void StartGame()
        {
            _isGameActive = true;
            _remainingTime = gameDurationSecs;
            _currentScore = 0;
            gameInput.EnableActionMap();
            spawnManager.StartSpawning();
            hudController.UpdateScore(_currentScore);
            hudController.UpdateTimer(_remainingTime);
        }

        public void PauseGame()
        {
            _isGameActive = false;
            spawnManager.StopSpawning();
            gameInput.DisableActionMap();
            uiController.ShowPause();
        }

        public void ResumeGame()
        {
            _isGameActive = true;
            gameInput.EnableActionMap();
            spawnManager.ResumeSpawning();
        }

        public void RestartGame()
        {
            ResetLevel();
            StartGame();
        }

        private void EndGame()
        {
            _isGameActive = false;
            spawnManager.StopSpawning();
            gameInput.DisableActionMap();
            UpdateHighScore();
            hudController.SetFinalScore(_currentScore);
            hudController.SetHighScore(GetHighScore());
            uiController.ShowGameOver();
        }

        public void ExitGame()
        {
            _isGameActive = false;
            spawnManager.StopSpawning();
            ResetLevel();
        }

        private void ResetLevel()
        {
            player.gameObject.transform.position = _startPosition;
            player.gameObject.transform.rotation = _startRotation;
            spawnManager.ClearSpawn();
        }
    
        private void OnCollectibleCollected(int points,  CollectibleItem _)
        {
            _currentScore += points;
            hudController.UpdateScore(_currentScore);
        }

        private void UpdateHighScore()
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                if (_currentScore > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", _currentScore);
                }
            }
            else
            {
                PlayerPrefs.SetInt("HighScore", _currentScore);
            }
        }

        private int GetHighScore()
        {
            return PlayerPrefs.GetInt("HighScore");
        }
    }
}
