using UnityEngine;

namespace RoninUtils.RoninCharacterController {


    /// <summary>
    /// Unity 中的 FSM 系统中，同一个clip上绑定的脚本，会按照顺序从上往下执行
    /// 利用这个特性，这里专门收集数据，供后续的脚本使用
    /// </summary>
    public class AnimState_CollectData : AnimStateBase {

        protected override void StartState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.StartState(data, cc, animator, stateInfo, layerIndex);
            mDataCollector.Collect();
        }


        protected override void UpdateState (RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            mDataCollector.Collect();
        }
    }
}
