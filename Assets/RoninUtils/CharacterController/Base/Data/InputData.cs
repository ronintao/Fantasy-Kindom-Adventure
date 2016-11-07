using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public class InputData {

        /// <summary>
        /// JoyStick 是否被使用
        /// </summary>
        public bool isJoyStickHold;


        /// <summary>
        /// joystick 的移动量
        /// </summary>
        public Vector2 joyStickMove;


        /// <summary>
        /// 开火
        /// </summary>
        public bool isFireing;


        /// <summary>
        /// 开火按住的时间
        /// </summary>
        public float fireHoldTime;


        /// <summary>
        /// 跳跃键按下
        /// </summary>
        public bool isJumpDown;


        /// <summary>
        /// 跳跃
        /// </summary>
        public bool isJump;


        /// <summary>
        /// 跳跃键按住的时间
        /// </summary>
        public float jumpHoldTime;

    }
}
