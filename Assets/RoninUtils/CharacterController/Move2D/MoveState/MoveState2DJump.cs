//using RoninUtils.Helper;
//using System;
//using UnityEngine;

//namespace RoninUtils.RoninCharacterController {


//    /// <summary>
//    /// 按一下就会跳起，如果按的时间长，则会加大跳跃力度
//    /// </summary>
//    public class MoveState2DJump : AnimStateBase {

//        [Tooltip("接受跳跃输入的时间，这段时间正好用来做起跳的动画")]
//        public float jumpAcceptTime = 0.2f;

//        [Tooltip("跳跃速度随按键时间的变化设定")]
//        public AnimationCurve jumpSpeedCureve;

//        private float mStartJumpTime;
//        private bool  mHoldingJump;
//        private float mHoldJumpTime;


//        protected override void StartState (RuntimeMoveData data, CharacterController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//            base.StartState(data, characterController, animator, stateInfo, layerIndex);
//            mStartJumpTime = Time.time;
//            mHoldJumpTime  = Time.time;
//            mHoldingJump = true;
//        }


//        protected override void UpdateState (RuntimeMoveData data, CharacterController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//            Vector3 currentSpeed = characterController.velocity;

//            // Normal State Check
//            if ( characterController.IsHitCeil() ) {
//                characterController.MoveWithSpeed(currentSpeed.x, Math.Min(currentSpeed.y, 0), 0);
//                animator.SetBool(AnimParamConstans.STATE_JUMP, false);
//                animator.SetBool(AnimParamConstans.STATE_IN_AIR, !characterController.isGrounded);
//                return;
//            }

//            mHoldingJump &= data.IsJump;
//            if (mHoldingJump)
//                mHoldJumpTime = Time.time;

//            if (Time.time > mStartJumpTime + jumpAcceptTime) {
//                characterController.MoveWithSpeed(currentSpeed.x, currentSpeed.y + jumpSpeedCureve.Evaluate(mHoldJumpTime - mStartJumpTime), 0);
//                animator.SetBool(AnimParamConstans.STATE_JUMP, false);

//            } else {
//                characterController.MoveWithSpeed(currentSpeed);
//            }

//        }

//    }
//}
