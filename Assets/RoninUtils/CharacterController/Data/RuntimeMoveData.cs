using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.RoninCharacterController {
    public class RuntimeMoveData {

        /// <summary>
        /// 输入信息，来自 Easy Touch
        /// </summary>
        public InputData inputData = new InputData();


        /// <summary>
        /// 碰撞信息，来自 Character Controller
        /// </summary>
        public CharacterControllerData ccData = new CharacterControllerData();


        /// <summary>
        /// 玩家能力，来自 Player
        /// </summary>
        public PlayerInfo playerInfo = new PlayerInfo();


        /// <summary>
        /// 动画信息，来自 Animation
        /// </summary>
        public AnimStateInfo animInfo = new AnimStateInfo();

    }
}
