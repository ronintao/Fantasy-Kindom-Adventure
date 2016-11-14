using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public abstract class AnimState_SetParam_Base : AnimStateBase {

        // return true if set the trigger
        protected bool SetTrigger(int triggerName, bool condition, Animator animator) {
            if (condition) {
                animator.SetTrigger(triggerName);
                return true;
            } else {
                animator.ResetTrigger(triggerName);
                return false;
            }
        }


        protected bool SetBool(int paramName, bool condition, Animator animator) {
            animator.SetBool(paramName, condition);
            return condition;
        }


    }
}
