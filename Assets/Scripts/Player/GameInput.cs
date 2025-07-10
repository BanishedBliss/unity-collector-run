using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
        }

        public void EnableActionMap()
        {
            _playerInputActions.Player.Enable();
        }

        public void DisableActionMap()
        {
            _playerInputActions.Player.Disable();
        }

        public bool GetPauseInput()
        {
            var pauseInput = _playerInputActions.Player.Pause;
            return pauseInput.WasPressedThisFrame();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
    
            return inputVector;
        }
    }
}
