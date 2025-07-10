using System;
using UnityEngine;

namespace UI
{
    /**
     * Manager class for UI transitions.
     */
    public class TransitionManager : MonoBehaviour
    {
        private static readonly int StartTransition = Animator.StringToHash("StartTransition");
        [SerializeField] private Animator animator;
        private Action _onBlack;
        private Action _onEnd;

        public void PlayTransition(Action onBlack = null, Action onEnd = null)
        {
            animator.gameObject.SetActive(true);
            _onBlack = onBlack;
            _onEnd = onEnd;
            animator.SetTrigger(StartTransition);
        }

        public void OnTransitionBlack()
        {
            _onBlack?.Invoke();
            _onBlack = null;
        }

        public void OnTransitionEnd()
        {
            _onEnd?.Invoke();
            _onEnd = null;
            animator.gameObject.SetActive(false);
        }
    }
}