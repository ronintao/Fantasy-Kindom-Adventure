using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {


    /// <summary>
    /// 在 Idle 状态，需要完成下面几件事：
    /// 等待一个随机的时间，播放special idle动画
    /// </summary>
    public class AnimState_Idle : AnimStateBase {

        [Tooltip("特殊Idle动画的种类（不包括普通Idle）")]
        public int SpecialAnimCount;

        [Tooltip("进入特殊Idle动画需要的最小等待时间")]
        public float minWaitTime;

        [Tooltip("进入特殊Idle动画需要的最大等待时间")]
        public float maxWaitTime;

        // 需要等待的时间
        private float mWaitTime;

        // 当前是否在播放特殊动画
        private bool mIsPlaySpecial;


        // 进入状态，这个函数只会在从其他状态进入本状态时被调用
        protected override void StartState (RuntimeMoveData data, RoninController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.StartState(data, characterController, animator, stateInfo, layerIndex);
            SetAsNormalIdleAnim(animator, 0);
        }

        // 更新状态
        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (mIsPlaySpecial && stateInfo.normalizedTime >= 1) {
                SetAsNormalIdleAnim(animator, stateInfo.normalizedTime);
            }

            if (!mIsPlaySpecial && stateInfo.normalizedTime > mWaitTime) {
                TriggerSpecialAnim(animator, stateInfo, layerIndex);
            }

            // change visual face
            float moveSpeed = data.ccData.velocity.x;
            if (moveSpeed > 0)
                cc.SetDirection(Vector3.right);
            else if (moveSpeed < 0)
                cc.SetDirection(Vector3.left);
        }


        // 设置为播放普通Idle动画模式
        private void SetAsNormalIdleAnim(Animator animator, float startTime) {
            mIsPlaySpecial = false;
            mWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime) + startTime;
            animator.SetFloat(AnimParamConstans.STATE_ANIM_INDEX, 0);
        }


        // 播放特殊动画
        private void TriggerSpecialAnim(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            mIsPlaySpecial = true;

            // Set Special Anim Clip
            int random = UnityEngine.Random.Range(1, SpecialAnimCount + 1); // random in [1, SpecialAnimCount]
            animator.SetFloat(AnimParamConstans.STATE_ANIM_INDEX, 1f * random / SpecialAnimCount);

            // Play From Start
            animator.Play(stateInfo.fullPathHash, layerIndex, 0);
        }

    }
}
