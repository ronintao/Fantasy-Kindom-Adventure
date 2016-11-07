using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {

    public static class PhysicsEX {


        #region Character Controller Enhancement

        /*************************************************
         * 
         * Character Controller Enhancement
         * 
         *************************************************/

        /// <summary>
        /// 复制 Character Controller 的基本属性
        /// </summary>
        public static void CopyTo(this CharacterController from, CharacterController to) {
            if (from == to || from == null || to == null)
                return;

            to.slopeLimit = from.slopeLimit;
            to.stepOffset = from.stepOffset;
            to.skinWidth  = from.skinWidth;
            to.center     = from.center;
            to.radius     = from.radius;
            to.height     = from.height;
        }

        /// <summary>
        /// 检测是否碰撞到了上方物体
        /// </summary>
        public static bool IsHitCeil (this CharacterController cc) {
            return (cc.collisionFlags & CollisionFlags.Above) != 0;
        }


        /// <summary>
        /// 按照指定的速度移动 Character Controller，会自动考虑重力
        /// </summary>
        public static void MoveWithSpeed(this CharacterController cc, Vector3 speed, bool useGravity = true) {
            speed = useGravity ? speed + Physics.gravity * Time.deltaTime : speed;
            cc.Move(speed * Time.deltaTime);
        }


        /// <summary>
        /// 按照指定的速度移动 Character Controller，会自动考虑重力
        /// </summary>
        public static void MoveWithSpeed(this CharacterController cc, float xSpeed, float ySpeed, float zSpeed, bool useGravity = true) {
            MoveWithSpeed(cc, new Vector3(xSpeed, ySpeed, zSpeed), useGravity);
        }


        #endregion // Character Controller Enhancement

    }
}
