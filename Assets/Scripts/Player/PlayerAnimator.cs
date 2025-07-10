using UnityEngine;

namespace Player
{
    public class PlayerAnimator: MonoBehaviour
    {
        private int _isWalking;

        [SerializeField] private PlayerController player;
        [SerializeField] private Animator animator;
        
        private void Awake()
        {
            _isWalking = Animator.StringToHash("IsWalking");
            animator.SetBool(_isWalking, player.IsWalking());
        }

        private void Update()
        {
            animator.SetBool(_isWalking, player.IsWalking());
        }
    }
}


