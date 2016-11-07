//using UnityEngine;
//using System.Collections;
//using System;

//namespace RoninUtils.RoninCharacterController {


//    /// <summary>
//    /// 这里是控制移动时的动画表现
//    /// </summary>
//    public class MoveAnimState2DMove : AnimStateBase {

//        protected override void UpdateState (RuntimeMoveData data, CharacterController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//            float moveSpeed = characterController.velocity.x;

//            // change visual face
//            if (moveSpeed > 0)
//                characterController.transform.forward = Vector3.right;
//            else if (moveSpeed < 0)
//                characterController.transform.forward = Vector3.left;


//            if (Math.Abs(data.JoyStickMove.x) > 0 || Math.Abs(moveSpeed) > 0 || data.IsJumpDown || data.IsFireing) {
//                animator.SetBool(AnimParamConstans.STATE_IDLE, false);
//            } else {
//                animator.SetBool(AnimParamConstans.STATE_IDLE, true);
//            }

//            animator.SetFloat(AnimParamConstans.STATE_RUNSPEED, Math.Abs(data.JoyStickMove.x));


//        }
//    }



//}

