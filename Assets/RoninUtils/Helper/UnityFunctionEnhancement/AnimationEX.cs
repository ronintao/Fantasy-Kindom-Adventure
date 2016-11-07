using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {


    public static class AnimationEX {


        /*************************************************
         * 
         * Animatior Enhancement
         * 
         *************************************************/

        /// <summary>
        /// 复制 Animator ，注意 trigger 是不能复制的（一般而言也不用复制）
        /// </summary>
        public static void CopyTo(this Animator from, Animator to) {
            if (from == to || from == null || to == null)
                return;

            // Copy Parameter
            AnimatorControllerParameter [] parameters = from.parameters;
            for (int i = 0; i < parameters.Length; i ++) {
                AnimatorControllerParameter param = parameters[i];
                switch (param.type) {
                    case AnimatorControllerParameterType.Bool:
                        to.SetBool(param.nameHash, from.GetBool(param.nameHash));
                        break;

                    case AnimatorControllerParameterType.Float:
                        to.SetFloat(param.nameHash, from.GetFloat(param.nameHash));
                        break;

                    case AnimatorControllerParameterType.Int:
                        to.SetInteger(param.nameHash, from.GetInteger(param.nameHash));
                        break;
                }
            }

            // Copy Current State
            for (int i = 0; i < from.layerCount; i ++) {
                AnimatorStateInfo info = from.GetCurrentAnimatorStateInfo(i);
                to.Play(info.fullPathHash, i, info.normalizedTime);
            }

            // Copy Transform
            from.transform.CopyTo(to.transform, true);
        }



        /*************************************************
         * 
         * Animation Curve Enhancement
         * 
         *************************************************/

        /// <summary>
        /// AnimationCurve 的x轴不能设置负值，这里的作用是，如果 time < 0 ，则取原点对称的值
        /// </summary>
        public static float EvaluateEx(this AnimationCurve curve, float time) {
            return Math.Sign(time) * curve.Evaluate( Math.Abs(time) );
        }




    }
}
