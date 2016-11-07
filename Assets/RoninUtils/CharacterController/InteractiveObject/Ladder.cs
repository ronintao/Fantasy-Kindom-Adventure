using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoninUtils.Helper;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {


    /// <summary>
    /// 这里对 ladder 做了一定简化
    /// 梯子一定是上下的，不存在歪的梯子
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class Ladder : RoninComponent {

        private BoxCollider mBoxCollider;



        protected override void Awake () {
            base.Awake();
            mBoxCollider = GetComponent<BoxCollider>();
        }


        /// <summary>
        /// 
        /// </summary>
        public float GetRelativePos() {
            return 0;
        }



    }
}
