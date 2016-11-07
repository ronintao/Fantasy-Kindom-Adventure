using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {

    public class AnimClipStateInfo {

        // 该 clip 的 hash
        public int fullPathHash;

        // gai
        public List<int> currentStateBehaviors = new List<int>();

        public AnimClipStateInfo (int fullPathHash, int behaviourHash) {
            this.fullPathHash = fullPathHash;
            this.currentStateBehaviors.Add(behaviourHash);
        }
    }


    public class AnimStateInfo {

        // Key 是 LayerIndex, Value 是该层当前的 clip 所拥有的脚本
        protected Dictionary<int, AnimClipStateInfo> mCurrentAnimState = new Dictionary<int, AnimClipStateInfo>();


        public bool IsInState (int layerIndex, int fullPathHash, int behaviourHash) {
            AnimClipStateInfo info = mCurrentAnimState.GetValueSafe(layerIndex);
            return info != null && info.fullPathHash == fullPathHash && info.currentStateBehaviors.Contains(behaviourHash);
        }


        public void SetInState (int layerIndex, int fullPathHash, int behaviourHash) {
            AnimClipStateInfo info = mCurrentAnimState.GetValueSafe(layerIndex);
            if (info != null && info.fullPathHash == fullPathHash) {
                info.currentStateBehaviors.AddIfUnRepeat(behaviourHash);
            } else {
                mCurrentAnimState.Set(layerIndex, new AnimClipStateInfo(fullPathHash, behaviourHash));
            }
        }


    }
}
