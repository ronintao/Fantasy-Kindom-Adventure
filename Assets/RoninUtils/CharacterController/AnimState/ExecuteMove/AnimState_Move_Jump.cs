using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {

    public class AnimState_Move_Jump : AnimStateBase {

        private float startJumpSpeed;


        protected override void StartState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.StartState(data, cc, animator, stateInfo, layerIndex);
            startJumpSpeed = data.playerInfo.moveSpeed.EvaluateEx(data.inputData.joyStickMove.x) / 2;
        }


        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            float jumpSpeed = data.playerInfo.jumpSpeed.Evaluate(stateInfo.normalizedTime);
            Vector3 current = data.ccData.velocity;
            cc.Move(new Vector3(MathHelper.MaxAbs(startJumpSpeed, current.x), jumpSpeed, 0), false);
        }
    }

}
