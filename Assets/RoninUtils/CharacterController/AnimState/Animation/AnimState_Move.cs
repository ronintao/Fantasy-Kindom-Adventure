using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public class AnimState_Move : AnimStateBase {

        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            // change visual face
            float moveSpeed = data.ccData.velocity.x;
            if (moveSpeed > 0)
                cc.SetDirection(Vector3.right);
            else if (moveSpeed < 0)
                cc.SetDirection(Vector3.left);
        }
    }
}
