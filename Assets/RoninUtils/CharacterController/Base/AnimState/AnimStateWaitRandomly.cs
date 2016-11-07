using UnityEngine;
using UnityEngine.Experimental.Director;

namespace RoninUtils.RoninCharacterController {

    public class AnimStateWaitRandomly : StateMachineBehaviour {

        [Tooltip("最小等待时间，单位是秒")]
        public float minWaitTime;

        [Tooltip("最大等待时间，单位是秒")]
        public float maxWaitTime;

        // 剩余的等待时间，当小于等于0时，会设置 TRIGGER_END_ANIM 这个 trigger
        private float mWaitTime;


        public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            mWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        }


        public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            if (mWaitTime <= 0)
                return;

            mWaitTime -= Time.deltaTime;
            if (mWaitTime <= 0)
                animator.SetTrigger(AnimParamConstans.TRIGGER_END_ANIM);
        }

    }
}
