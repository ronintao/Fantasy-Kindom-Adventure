using System;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {


    public class AnimState_Move_Slide : AnimStateBase {

        // 由于 slide 被切分成多块，因此需要分别设定

        [Tooltip("对应的滑铲速度时间曲线的起始时间（归一化的）")]
        public float startTime;

        [Tooltip("对应的滑铲速度时间曲线的终止时间（归一化的）")]
        public float endTime;


        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            float slideV = data.playerInfo.slideSpeed.Evaluate(Mathf.Lerp(startTime, endTime, stateInfo.normalizedTime));

            Vector3 currentV = data.ccData.velocity;
            currentV.x = Math.Sign(cc.GetDirection().x) * slideV;

            cc.Move(currentV);
        }
    }
}
