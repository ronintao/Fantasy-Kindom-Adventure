using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Experimental.Director;

namespace RoninUtils.RoninCharacterController {


    /// <summary>
    /// 选择一个随机的Clip播放
    /// </summary>
    public class AnimStateChooseClipRandom : StateMachineBehaviour {

        [Tooltip("Clip 种类数量")]
        public int ClipCount;


        public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            int random = UnityEngine.Random.Range(0, ClipCount - 1);
            float blendValue = 1f / (ClipCount - 1) * random;
            animator.SetFloat(AnimParamConstans.STATE_ANIM_INDEX, blendValue);
        }
    }



}

