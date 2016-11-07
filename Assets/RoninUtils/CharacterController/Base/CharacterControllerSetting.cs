using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.RoninCharacterController {

    // 记录 Character Controller 的参数
    // 当前 Min Move Distance 不能通过代码修改，因此这里不记录
    [Serializable]
    public class CharacterControllerSettingItem {

        [Tooltip("该Setting对应的状态")]
        public string name;

        [Tooltip("爬坡角度限制. [0  180]")]
        public float slopeLimit;

        public float stepOffset;

        public float skinWidth;

        public Vector3 center;

        public float radius;

        public float height;


        /// <summary>
        /// 将参数复制到 CharacterController 上
        /// Ronin : CFNM  && DPMX
        /// </summary>
        public void CopyTo(CharacterController cc) {
            cc.slopeLimit = slopeLimit;
            cc.stepOffset = stepOffset;
            cc.skinWidth  = skinWidth;
            cc.center     = new Vector3(0, height/2, 0);
            cc.radius     = radius;
            cc.height     = height;
        }

    }



    /// <summary>
    /// 
    /// </summary>
    public class CharacterControllerSetting : RoninComponent {

        [HideInInspector]
        public string CurrentState = CCStateConstants.CC_STATE_DEFAULT;

        public CharacterControllerSettingItem [] settings;

        private Dictionary<string, CharacterControllerSettingItem> mSettingMap = new Dictionary<string, CharacterControllerSettingItem>();

        protected override void Awake () {
            base.Awake();
            settings.ValueForeach(item => mSettingMap.Add(item.name, item));
        }

        public CharacterControllerSettingItem ActiveSetting (string state = CCStateConstants.CC_STATE_DEFAULT) {
            CurrentState = state;
            return mSettingMap.GetValueSafe(CurrentState, null);
        }

    }

}
