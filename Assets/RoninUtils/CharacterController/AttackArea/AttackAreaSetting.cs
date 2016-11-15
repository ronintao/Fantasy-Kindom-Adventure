using UnityEngine;
using System.Collections.Generic;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {


    public class AttackAreaSetting : RoninComponent {

        // 这个数据来自于子节点的汇集
        [SerializeField]
        public AttackAreaItem [] attackItems;

        // Lasy Initialized
        private Dictionary<AttackAreaType, List<AttackAreaItem>> attackAreaItemMap = null;
        private void ParseAttackAreaSetting() {
            attackAreaItemMap = new Dictionary<AttackAreaType, List<AttackAreaItem>>();
            foreach (AttackAreaItem item in attackItems) {
                item.attackTypes.ValueForeach(type => attackAreaItemMap.AddListItem(type, item));
            }
        }

        public Dictionary<AttackAreaType, List<AttackAreaItem>> GetAttackAreaSetting() {
            if (attackAreaItemMap == null)
                ParseAttackAreaSetting();

            return attackAreaItemMap;
        }



    }


}

