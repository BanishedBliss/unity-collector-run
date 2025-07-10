using UnityEngine;
using Utils;

namespace UI.Transition
{
    public class TransitionBlackController : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var manager = animator.GetComponent<TransitionManager>();
            if (manager != null)
                manager.OnTransitionBlack();
        }
    }
}
