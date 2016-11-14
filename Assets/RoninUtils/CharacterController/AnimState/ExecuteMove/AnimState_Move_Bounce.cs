using RoninUtils.Helper;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public class AnimState_Move_Bounce : AnimStateBase {

        protected override void StartState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.StartState(data, cc, animator, stateInfo, layerIndex);

            Vector3 current = data.ccData.velocity;
            current = current.SetY(data.playerInfo.jumpSpeed.Evaluate(1) * data.ccData.bouncingFactor);
            cc.Move(current, false);
        }


        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            cc.Move(data.ccData.velocity);
        }
    }
}
