using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {
    public class AnimState_Move_OnLand : AnimStateBase {

        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            // Move
            float moveSpeed = data.playerInfo.moveSpeed.EvaluateEx(data.inputData.joyStickMove.x);
            cc.Move(moveSpeed, 0, 0);
        }

    }
}
