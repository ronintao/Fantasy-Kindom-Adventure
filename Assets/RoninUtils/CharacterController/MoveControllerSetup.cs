using RoninUtils.Helper;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    /// <summary>
    /// 这个类是一个配置表，用来找到并初始化各个组件
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class MoveControllerSetup : RoninComponent {

        /*************************************************
         * 
         * Fields
         * 
         *************************************************/

        // 数据采集者，需要在本类（或子类）中 create
        private DataCollector mDataCollector;

        // 动画的执行者，需要在本 gameobject 上绑定
        private Animator mAnimator;

        // 外观的根节点，作为子节点存在，也可能来自于外部设入
        private GameObject mVisual;

        // CC 的数据配置者，来自于 Visual 节点
        private CharacterControllerSetting mCCSetting;

        // 移动的执行者
        private RoninController mCC;

        // 行动能力限定，来自 CC
        private PlayerAbility mAbility;


        /*************************************************
         * 
         * Life Circle
         * 
         *************************************************/


        /// <summary>
        /// 启动配置，获得各个配置来源
        /// </summary>
        internal virtual void Init(RoninController controller, MoveControllerSetup predecessor,
                string initCCState = CCStateConstants.CC_STATE_DEFAULT, bool inheritVisual = false, bool inheritAnimator = false) {

            // Init Transform
            this.SetParent(controller);
            this.transform.Reset();
            gameObject.SetActive(true);

            // Init Visual
            if (inheritVisual) {
                mVisual = predecessor.mVisual;
                mVisual.SetParent(this);
            } else {
                mVisual = transform.GetChild(0).gameObject;
            }

            // Init CC Setting
            mCCSetting = mVisual.GetComponent<CharacterControllerSetting>();

            // Init CC
            mCC = controller;
            mCC.SetCCParams(mCCSetting.ActiveSetting(initCCState));

            // Init Ability
            mAbility = mCC.GetComponent<PlayerAbility>();

            // Init DataCollector
            mDataCollector = mDataCollector ?? CreateDataCollector();
            mDataCollector.Init(this);

            // Init Animator
            mAnimator = mAnimator ?? GetComponent<Animator>();
            if (inheritAnimator)
                predecessor.mAnimator.CopyTo(mAnimator);
        }

        /// <summary>
        /// 创建数据收集者，可以由子类复写
        /// </summary>
        protected virtual DataCollector CreateDataCollector () {
            return new DataCollector();
        }


        /// <summary>
        /// 停止配置，释放引用
        /// </summary>
        internal void Stop() {
            gameObject.SetActive(false);
            mVisual = null;
            mCC     = null;
        }





        /*************************************************
         * 
         * Getter
         * 
         *************************************************/

        internal DataCollector GetDataCollector () {
            return mDataCollector;
        }

        internal Animator GetAnimator() {
            return mAnimator;
        }

        internal GameObject GetVisual () {
            return mVisual;
        }

        internal RoninController GetCharacterController () {
            return mCC;
        }

        internal PlayerAbility GetPlayerAbility() {
            return mAbility;
        }

        /// <summary>
        /// 用来接收 animation 或是外界的事件
        /// </summary>
        public void ChangeCCConfig(string mode) {
            if (mCC != null)
                mCC.SetCCParams(mCCSetting.ActiveSetting(mode));
        }

    }
}
