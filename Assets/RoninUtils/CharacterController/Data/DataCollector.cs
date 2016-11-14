using RoninUtils.Helper;
using System.Collections.Generic;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {


    public class DataCollector {

        // Set Up 用来获取各个分量
        protected MoveControllerSetup mSetup;

        // 实时数据，本类的核心操作数据
        protected RuntimeMoveData mData = new RuntimeMoveData();

        public void Init(MoveControllerSetup setup) {
            mSetup = setup;
        }

        public virtual void UpdateAnimState(int layerIndex, int fullPathHash, int behaviourHash) {
            mData.animInfo.SetInState(layerIndex, fullPathHash, behaviourHash);
        }


        public virtual void Collect(bool collectInput = true, bool collectCollision = true) {
            if (collectInput)
                CollectInputData();

            if (collectCollision)
                CollectCollisionData();

            CollectPlayerInfoData();
        }


        public RuntimeMoveData GetData() {
            return mData;
        }


        #region Collect Input Data


        protected virtual void CollectInputData() {
            InputData inputData = mData.inputData;
            inputData.isJoyStickHold = ETCInput.GetControlJoystick(InputConstants.JOYSTICK_NAME).visible;

            float horizontalMove   = ETCInput.GetAxis(InputConstants.AXIS_HORIZONTAL);
            float verticleMove     = ETCInput.GetAxis(InputConstants.AXIS_VERTICAL);
            inputData.joyStickMove = new Vector2(horizontalMove, verticleMove);

            inputData.isFireing    = ETCInput.GetButton        (InputConstants.BUTTON_FIRE);
            inputData.fireHoldTime = ETCInput.GetButtonDownTime(InputConstants.BUTTON_FIRE);


            inputData.isJumpDown   = ETCInput.GetButtonDown    (InputConstants.BUTTON_JUMP);
            inputData.isJump       = ETCInput.GetButton        (InputConstants.BUTTON_JUMP);
            inputData.jumpHoldTime = ETCInput.GetButtonDownTime(InputConstants.BUTTON_JUMP);
        }

        #endregion


        #region Collect Collision Data

        protected virtual void CollectCollisionData() {
            RoninController             ccWrap = mSetup.GetCharacterController();
            CharacterController         cc     = ccWrap.GetCCImpl();
            List<ControllerColliderHit> hits   = ccWrap.GetLastHits();

            CharacterControllerData collisionData = mData.ccData;

            collisionData.collisionFlags = cc.collisionFlags;
            collisionData.velocity       = cc.velocity;
            collisionData.isGrounded     = cc.isGrounded && IsGround(hits);
            collisionData.isAtLadder     = false;
            collisionData.isBouncing     = cc.isGrounded && IsBouncing(hits, ref collisionData.bouncingFactor);
        }

        private static bool IsGround(List<ControllerColliderHit> hits) {
            bool isGrounded = false;
            hits.ValueForEach(hit => {
                if (LayerDefine.SceneCollideA.IsLayer(hit.gameObject) && hit.normal.IsPointUp())
                    isGrounded = true;
            });
            return isGrounded;
        }


        // 使用 physics material 来检查 bounce 信息
        private static bool IsBouncing(List<ControllerColliderHit> hits, ref float bouncingFactor) {
            BoucingObject bouce;
            for (int i = 0; i < hits.Count; i ++) {
                if ( hits[i].normal.IsPointUp() && (bouce = hits[i].gameObject.GetComponent<BoucingObject>()) != null ) {
                    bouncingFactor = bouce.bouncingFactor;
                    return true;
                }
            }

            return false;
        }



        #endregion


        #region Collect Player Info

        private void CollectPlayerInfoData() {
            PlayerInfo playerInfoData = mData.playerInfo;
            PlayerAbility ability = mSetup.GetPlayerAbility();

            playerInfoData.canSecondJump = ability.Can2ndJump;
            playerInfoData.moveSpeed     = ability.moveSpeed;
            playerInfoData.jumpSpeed     = ability.jumpSpeed;
            playerInfoData.maxJumpSpeed  = ability.maxJumpSpeed;
            playerInfoData.slideSpeed    = ability.slideSpeed;
        }



        #endregion

    }
}
