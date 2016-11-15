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
        internal DataCollector dataCollector { get; private set; }

        // 动画的执行者，需要在本 gameobject 上绑定
        internal Animator animator { get; private set; }

        // 外观的根节点，作为子节点存在，也可能来自于外部设入
        internal GameObject visual { get; private set; }

        // CC 的数据配置者，来自于 Visual 节点
        internal CharacterControllerSetting ccSetting { get; private set; }

        // Attack Area 的数据配置，来自于 Visual 节点
        internal AttackAreaSetting attackAreaSetting { get; private set; }

        // 移动的执行者
        internal RoninController cc { get; private set; }

        // 行动能力限定，来自 CC
        internal PlayerAbility ability { get; private set; }


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
                visual = predecessor.visual;
                visual.SetParent(this);
            } else {
                visual = transform.GetChild(0).gameObject;
            }

            // Init CC Setting
            ccSetting = visual.GetComponent<CharacterControllerSetting>();

            // Init Attack Area
            attackAreaSetting = visual.GetComponent<AttackAreaSetting>();

            // Init CC
            cc = controller;
            cc.SetCCParams(ccSetting.ActiveSetting(initCCState));

            // Init Ability
            ability = cc.GetComponent<PlayerAbility>();

            // Init DataCollector
            dataCollector = dataCollector ?? CreateDataCollector();
            dataCollector.Init(this);

            // Init Animator
            animator = animator ?? GetComponent<Animator>();
            if (inheritAnimator)
                predecessor.animator.CopyTo(animator);
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
            visual = null;
            cc     = null;
        }



        /*************************************************
         * 
         * Event Hander
         * 
         *************************************************/

        /// <summary>
        /// 用来接收 animation 或是外界的事件
        /// </summary>
        public void ChangeCCConfig(string mode) {
            if (cc != null)
                cc.SetCCParams(ccSetting.ActiveSetting(mode));
        }

    }
}
