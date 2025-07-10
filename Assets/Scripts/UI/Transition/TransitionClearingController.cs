using UnityEngine;
using Utils;

namespace UI.Transition
{
    public class TransitionClearingController : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var manager = animator.GetComponent<TransitionManager>();
            if (manager != null)
                manager.OnTransitionEnd();
        }
    }
}
