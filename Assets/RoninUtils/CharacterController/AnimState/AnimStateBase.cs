using UnityEngine;
using System.Collections;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {


    public abstract class AnimStateBase : StateMachineBehaviour {

        private int mHashCode;

        protected MoveControllerSetup mConfig;
        protected DataCollector       mDataCollector;
        protected RoninController     mMoveImpl;
        protected RuntimeMoveData     mMoveData { get { return mDataCollector.GetData(); } }

        protected virtual bool needCollectData { get { return true; } }

        /**
         * OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
         */
        public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            mHashCode = GetHashCode();

            mConfig = animator.GetComponent<MoveControllerSetup>();
            mDataCollector = mConfig.dataCollector;
            mMoveImpl      = mConfig.cc;

            if ( mMoveData.animInfo.IsInState(layerIndex, stateInfo.fullPathHash, mHashCode) )
                return;

            mDataCollector.UpdateAnimState(layerIndex, stateInfo.fullPathHash, mHashCode);
            StartState(mMoveData, mMoveImpl, animator, stateInfo, layerIndex);
        }


        /**
         * OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
         */
        public sealed override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (mConfig == null || !mMoveData.animInfo.IsInState(layerIndex, stateInfo.fullPathHash, mHashCode))
                return;

            UpdateState(mMoveData, mMoveImpl, animator, stateInfo, layerIndex);
        }


        /// <summary>
        /// Start 反复重入时不会反复调用
        /// </summary>
        protected virtual void StartState(RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }


        /// <summary>
        /// Update 只有在当前 Clip 是自己的时候才会调用
        /// </summary>
        protected abstract void UpdateState(RuntimeMoveData data, RoninController cc, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }

}

