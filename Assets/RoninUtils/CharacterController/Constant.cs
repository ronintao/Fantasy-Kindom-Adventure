using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public static class InputConstants {

        public const string JOYSTICK_NAME   = "Joystick";

        public const string AXIS_HORIZONTAL = "Horizontal";

        public const string AXIS_VERTICAL   = "Vertical";

        public const string BUTTON_FIRE     = "Fire";

        public const string BUTTON_JUMP     = "Jump";
    }


    public static class CCStateConstants {

        public const string CC_STATE_DEFAULT = "Default";

        public const string CC_STATE_SQUAT   = "Squat";

        public const string CC_STATE_SLIDE   = "Slide";
    }


    public static class AnimParamConstans {

        public static readonly int STATE_JUMP       = Animator.StringToHash("Jump");

        public static readonly int STATE_JUMP_2ND   = Animator.StringToHash("Jump2nd");

        public static readonly int STATE_RUNSPEED   = Animator.StringToHash("RunSpeed");

        public static readonly int STATE_IN_AIR     = Animator.StringToHash("InAir");

        public static readonly int STATE_SLIDE      = Animator.StringToHash("Slide");

        public static readonly int STATE_IDLE       = Animator.StringToHash("Idle");

        public static readonly int STATE_BOUNCING   = Animator.StringToHash("Bouncing");

        public static readonly int STATE_ANIM_INDEX = Animator.StringToHash("AnimIndex");

        public static readonly int TRIGGER_END_ANIM = Animator.StringToHash("EndAnimTrigger");

        public static readonly int STATE_HURT       = Animator.StringToHash("Hurt");

        public static readonly int STATE_DIE        = Animator.StringToHash("Die");

        public static readonly int STATE_REVIVE     = Animator.StringToHash("Revive");
    }
}
