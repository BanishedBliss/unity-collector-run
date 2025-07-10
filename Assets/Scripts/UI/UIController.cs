using Core;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject hud;
        [SerializeField] private TransitionManager transitionManager;
        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject pauseScreen;

        private void Awake()
        {
            ShowMainMenu();
        }
        
        private void ShowMainMenu()
        {
            mainMenuScreen.SetActive(true);
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }

        public void ShowGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        public void ShowPause()
        {
            pauseScreen.SetActive(true);
        }

        private void ShowHud()
        {
            hud.SetActive(true);
        }
        
        private void HideAllScreens()
        {
            mainMenuScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
            ShowHud();
        }

        public void StartGame()
        {
            transitionManager.PlayTransition(
                onBlack: () => {
                    gameManager.StartGame();
                    HideAllScreens();
                });
        }

        public void ResumeGame()
        {
            gameManager.ResumeGame();
            HideAllScreens();
        }

        public void RestartGame()
        {
            transitionManager.PlayTransition(
                onBlack: () => {
                    gameManager.RestartGame();
                    HideAllScreens();
                });
        }

        public void MainMenu()
        {
            transitionManager.PlayTransition(
                onBlack: () => {
                    gameManager.ExitGame();
                    ShowMainMenu();
                });
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
