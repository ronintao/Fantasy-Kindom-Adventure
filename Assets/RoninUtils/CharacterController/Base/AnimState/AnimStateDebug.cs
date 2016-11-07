using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {
    public class AnimStateDebug : AnimStateBase {

        protected override bool needCollectData { get { return false; } }

        protected override void StartState (RuntimeMoveData data, RoninController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.StartState(data, characterController, animator, stateInfo, layerIndex);
            Debug.Log("Enter State " + stateInfo.fullPathHash);
        }

        protected override void UpdateState (RuntimeMoveData data, RoninController characterController, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Debug.Log("Update State " + stateInfo.fullPathHash);
        }
    }
}
