using Core;
using UnityEngine;

namespace Player
{
    public class PlayerController: MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private float playerRadius = .4f;
        [SerializeField] private float playerHeight = 2f;
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotateSpeed = 10f;
        [Header("Setup")]
        [SerializeField] private GameInput gameInput;
        [SerializeField] private LayerMask physicsLayerMask;
        [SerializeField] private GameManager gameManager;
    
        private bool _isWalking;

        private void Update()
        {
            if (gameManager.GetIsGameActive())
            {
                ReceiveMovementInput();
                ReceivePauseInput();
            }
        }

        public bool IsWalking()
        {
            return _isWalking;
        }

        private void ReceivePauseInput()
        {
            if (gameInput.GetPauseInput())
            {
                gameManager.PauseGame();
            }
        }

        /**
         * Basic manual controls for player.
         */
        private void ReceiveMovementInput()
        {
            var inputVector = gameInput.GetMovementVectorNormalized();
            var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            var moveDistance = moveSpeed * Time.deltaTime;
            var canMove = !Physics.CapsuleCast(transform.position, 
                transform.position + Vector3.up * playerHeight, 
                playerRadius, moveDir, moveDistance, physicsLayerMask);
        
            if (!canMove)
            {
                TryHugObstacle(ref canMove, ref moveDir, moveDistance);
            }
            if (canMove) 
            {
                transform.position += moveDir * (moveSpeed * Time.deltaTime);
            }
        
            _isWalking = moveDir != Vector3.zero;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

        /**
         * Behaviour in case of facing an obstacle.
         */
        private void TryHugObstacle(ref bool canMove, ref Vector3 moveDir, float moveDistance)
        {
            // Try to move in parallel to the obstacle along X axis.
            Vector3 moveDirX = new  Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, 
                transform.position + Vector3.up * playerHeight, 
                playerRadius, moveDirX, moveDistance, physicsLayerMask);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Try to move in parallel to the obstacle along Z axis.
                Vector3 moveDirZ = new  Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, 
                    transform.position + Vector3.up * playerHeight, 
                    playerRadius, moveDirZ, moveDistance, physicsLayerMask);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
    }
}

