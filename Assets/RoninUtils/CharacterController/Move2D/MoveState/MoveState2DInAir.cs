//using RoninUtils.Helper;
//using System;
//using UnityEngine;

//namespace RoninUtils.RoninCharacterController {


//    public class MoveState2DInAir : AnimStateBase {

//        /**
//         * 
//         */
//        protected override void UpdateState (RuntimeMoveData data, CharacterController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//            Vector3 currentSpeed = characterController.velocity;

//            // Move
//            if ( characterController.IsHitCeil() ) {
//                characterController.MoveWithSpeed(currentSpeed.x, Math.Min(currentSpeed.y, 0), 0);
//            } else {
//                characterController.MoveWithSpeed(currentSpeed);
//            }

//            //Set Flag
//            if (characterController.isGrounded) {
//                animator.SetBool(AnimParamConstans.STATE_IN_AIR, false);
//            }
//        }
//    }
//}
