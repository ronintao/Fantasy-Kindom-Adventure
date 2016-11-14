using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public class AnimState_Move_Land : AnimStateBase {

        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Vector3 current = data.ccData.velocity;
            cc.Move(new Vector3(0, current.y, 0));
        }
    }
}
