using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    /// <summary>
    /// 在陆地上，需要设置以下几个参数（按键的优先级更高）：
    /// 1. 是否起跳，若是，设置 STATE_JUMP
    /// 2. 是否滑铲，若是，设置 STATE_SLIDE
    /// 3. 是否爬梯，若是，设置 STATE_CLIMB
    /// 4. 是否悬空，若是，设置 STATE_FALL
    /// 5. 是否移动，若是，设置 STATE_RUNSPEED 的大小 [-1 1]
    /// 6. 是否无输入，若是，设置 STATE_IDLE
    /// </summary>
    public class AnimState_SetParam_OnLand : AnimState_SetParam_Base {


        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            // 移动
            animator.SetFloat(AnimParamConstans.STATE_RUNSPEED, data.inputData.joyStickMove.x);

            // 起跳
            if (SetTrigger(AnimParamConstans.STATE_JUMP, data.inputData.isJumpDown, animator))
                return;

            // 滑铲
            if (SetTrigger(AnimParamConstans.STATE_SLIDE, data.inputData.isFireing, animator))
                return;

            if (SetBool(AnimParamConstans.STATE_IN_AIR, !data.ccData.isGrounded, animator))
                return;

            // 无输入
            animator.SetBool(AnimParamConstans.STATE_IDLE, !data.inputData.isJoyStickHold);
        }
    }
}
