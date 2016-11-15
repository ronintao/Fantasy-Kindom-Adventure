using RoninUtils.Helper;
using UnityEngine;
using System.Collections.Generic;

namespace RoninUtils.RoninCharacterController {


    /// <summary>
    /// Character Controller （以下简称为CC）的 Wrap
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public sealed class RoninController : RoninComponent{

        [Tooltip("SetUp 是整套配置，包括动画，参数等等")]
        [SerializeField]
        private MoveControllerSetup mSetup;

        // 实际的执行者
        private CharacterController mCCImpl;

        // 上一次执行 Move 时，发生的碰撞信息
        private List<ControllerColliderHit> mLastHits = new List<ControllerColliderHit>();


        #region Life Circle

        protected override void Awake () {
            base.Awake();

            mCCImpl = GetComponent<CharacterController>();

            InitSetup(mSetup, null, false, false);
        }

        protected override void OnDestroy () {
            base.OnDestroy();
            DestroySetup(mSetup);
            mSetup = null;
        }

        #endregion // Life Circle




        /*************************************************
         * 
         * SetUp Controll
         * 
         *************************************************/


        //
        private void InitSetup(MoveControllerSetup setup, MoveControllerSetup predecessor, bool inheritVisual, bool inheritAnimator) {
            if (setup == null)
                return;

            setup.Init(this, predecessor, CCStateConstants.CC_STATE_DEFAULT, inheritVisual, inheritAnimator);
        }

        //
        private void DestroySetup(MoveControllerSetup setup) {
            if (setup != null)
                setup.Stop();
        }

        /// <summary>
        /// 复制 CC 的参数
        /// </summary>
        public void SetCCParams(CCCollideSettingItem paramSetting) {
            paramSetting.CopyTo(mCCImpl);
        }



        /*************************************************
         * 
         * Move Enhancement && Collision Detect
         * 
         *************************************************/

        public void Move(float xSpeed, float ySpeed, float zSpeed, bool useGravity = true) {
            mLastHits.Clear();
            mCCImpl.MoveWithSpeed(xSpeed, ySpeed, zSpeed, useGravity);
        }


        public void Move(Vector3 speed, bool useGravity = true) {
            mLastHits.Clear();
            mCCImpl.MoveWithSpeed(speed, useGravity);
        }

        public void SetDirection (Vector3 direction) {
            transform.forward = direction;
        }

        public Vector3 GetDirection() {
            return transform.forward;
        }


        // OnControllerColliderHit is called when the controller hits a collider while performing a Move.
        void OnControllerColliderHit (ControllerColliderHit hit) {
            mLastHits.AddIfUnRepeat(hit);
        }

        // OnTriggerStay is called almost all the frames for every Collider other that is touching the trigger.
        void OnTriggerEnter (Collider other) {
        }

        // OnTriggerExit is called when the Collider other has stopped touching the trigger.
        void OnTriggerExit (Collider other) {
        }



        /*************************************************
         * 
         * Character Controller & Collision Data
         * 
         *************************************************/

        // 需要从 CC 中获取一些信息
        public CharacterController GetCCImpl() {
            return mCCImpl;
        }

        public List<ControllerColliderHit> GetLastHits() {
            return mLastHits;
        }
    }
}
