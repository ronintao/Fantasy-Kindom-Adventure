using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoninUtils.Helper;
using UnityEngine;


namespace RoninUtils.RoninCharacterController {
    public class PlayerAbility : RoninComponent {

        [Tooltip("是否可以二段跳")]
        public bool Can2ndJump = false;

        [Tooltip("移动速度和输入的关系")]
        public AnimationCurve moveSpeed;

        [Tooltip("跳起速度和归一化时间的关系")]
        public AnimationCurve jumpSpeed;

        // 最大跳起速度（取结尾时的速度）
        public float maxJumpSpeed { get; private set; }

        [Tooltip("滑铲速度和归一化时间的关系")]
        public AnimationCurve slideSpeed;


        protected override void Awake () {
            base.Awake();
            maxJumpSpeed = jumpSpeed[jumpSpeed.length - 1].value;
        }

    }
}
