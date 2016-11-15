using UnityEngine;
using System.Collections.Generic;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {


    public class AttackAreaSetting : RoninComponent {

        [SerializeField]
        public AttackAreaItem [] attackItems;

        public Dictionary<AttackAreaType, List<AttackAreaItem>> attackItemMap = new Dictionary<AttackAreaType, List<AttackAreaItem>>();


        protected override void Awake () {
            base.Awake();
        }
    }


}

