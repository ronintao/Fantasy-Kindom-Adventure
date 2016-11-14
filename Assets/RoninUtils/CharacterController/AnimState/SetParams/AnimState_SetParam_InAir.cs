using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    /// <summary>
    /// 在下落过程中，需要设置以下几个参数（按键的优先级更高）：
    /// 1. 是否爬绳子
    /// 2. 是否二段跳
    /// 3. 是否踩中弹簧
    /// 4. 是否落地
    /// 5. 是否移动
    /// </summary>
    public class AnimState_SetParam_InAir : AnimState_SetParam_Base {

        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            // 移动
            animator.SetFloat(AnimParamConstans.STATE_RUNSPEED, data.inputData.joyStickMove.x);

            // 二段跳
            if (SetTrigger(AnimParamConstans.STATE_JUMP_2ND, data.playerInfo.canSecondJump && data.inputData.isJumpDown, animator))
                return;

            // 弹簧
            if (SetTrigger(AnimParamConstans.STATE_BOUNCING, data.ccData.isBouncing, animator))
                return;

            // 落地
            animator.SetBool(AnimParamConstans.STATE_IN_AIR, !data.ccData.isGrounded);
        }

    }
}
