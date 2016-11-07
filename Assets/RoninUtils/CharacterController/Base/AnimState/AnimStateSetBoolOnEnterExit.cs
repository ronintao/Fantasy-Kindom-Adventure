using UnityEngine;

namespace RoninUtils.RoninCharacterController {


    public class AnimStateSetBoolOnEnterExit : StateMachineBehaviour {

        public string paramName;

        public bool valueForEnter;

        public bool valueForExit;

        public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetBool(paramName, valueForEnter);
        }

        public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateExit(animator, stateInfo, layerIndex);
            animator.SetBool(paramName, valueForExit);
        }

    }



}

