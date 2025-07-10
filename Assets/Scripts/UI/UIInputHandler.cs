using UnityEngine;

namespace UI
{
    public class UIInputHandler : MonoBehaviour
    {
        [SerializeField] private UIController uiController;

        public void OnStartButtonClicked() => uiController.StartGame();
        public void OnResumeButtonClicked() => uiController.ResumeGame();
        public void OnRestartButtonClicked() => uiController.RestartGame();
        public void OnMainMenuButtonClicked() => uiController.MainMenu();
        public void OnExitButtonClicked() => uiController.ExitGame();
    }
}