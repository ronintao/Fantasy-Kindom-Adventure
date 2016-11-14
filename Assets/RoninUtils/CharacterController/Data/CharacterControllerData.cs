using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {
    public class CharacterControllerData {

        /// <summary>
        /// 碰撞信息
        /// </summary>
        public CollisionFlags collisionFlags;


        /// <summary>
        /// 当前的移动速度
        /// </summary>
        public Vector3 velocity;


        /// <summary>
        /// 是否在地面上
        /// </summary>
        public bool isGrounded;


        /// <summary>
        /// 是否接触到了梯子
        /// </summary>
        public bool isAtLadder;

        /// <summary>
        /// 是否在弹簧上
        /// </summary>
        public bool isBouncing;


        /// <summary>
        /// 反弹系数
        /// </summary>
        public float bouncingFactor;


        /// <summary>
        /// 是否受伤
        /// </summary>
        public bool isHurt;


        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool isDie;


        /// <summary>
        /// 是否复活
        /// </summary>
        public bool isRevive;

    }
}
