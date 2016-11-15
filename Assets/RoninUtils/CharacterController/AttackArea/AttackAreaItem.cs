using RoninUtils.Helper;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {

    public enum AttackAreaType {

        // 拳
        FIST,

        // 踩踏
        TRAMPLE,

        // 踢
        KICK
    }


    [RequireComponent(typeof(Collider))]
    public class AttackAreaItem : RoninComponent {

        public AttackAreaType[] attackTypes;

        private Collider mCollider;

        protected override void Awake () {
            base.Awake();
            mCollider = GetComponent<Collider>();
        }


        /// <summary>
        /// 判断是否在碰撞盒内
        /// </summary>
        public bool IsInArea(Vector3 position) {
            return mCollider.bounds.Contains(position);
        }


    }
}
