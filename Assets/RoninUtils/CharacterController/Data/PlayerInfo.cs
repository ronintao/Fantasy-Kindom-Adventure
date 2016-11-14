using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public class PlayerInfo {

        /// <summary>
        /// 可以二段跳
        /// </summary>
        public bool canSecondJump;

        /// <summary>
        /// 移动速度和输入的关系
        /// </summary>
        public AnimationCurve moveSpeed;

        /// <summary>
        /// 起跳速度和时间的关系
        /// </summary>
        public AnimationCurve jumpSpeed;

        /// <summary>
        /// 最大跳跃速度
        /// </summary>
        public float maxJumpSpeed;

        /// <summary>
        /// 滑铲速度和时间的关系
        /// </summary>
        public AnimationCurve slideSpeed;
    }
}
