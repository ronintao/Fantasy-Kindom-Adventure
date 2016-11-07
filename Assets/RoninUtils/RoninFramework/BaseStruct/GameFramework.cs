using RoninUtils.Helper;
using System.Collections.Generic;
using UnityEngine;


namespace RoninUtils.RoninFramework {

    public partial class GameFramework : RoninSingletonComponent<GameFramework> {

        /// <summary>
        /// key 是 GameService 类名的 hash 值，值是对应的Service实例
        /// </summary>
        [SerializeField]
        private Dictionary<System.Type, GameService> mGameServices = new Dictionary<System.Type, GameService>();


        protected override void Awake () {
            base.Awake();

            // DontDestroyOnLoad 是将整个 object 树移入一个不销毁的 Scene，因此子节点也享受这个待遇
            DontDestroyOnLoad(this.gameObject);

            AddAllServices();
            mGameServices.ValueForeach( service => service.Init() );
        }


        protected override void Update () {
            base.Update();
            mGameServices.ValueForeach( service => service.Update() );
        }


        protected override void FixedUpdate () {
            base.FixedUpdate();
            mGameServices.ValueForeach( service => service.FixedUpdate() );
        }

    }


}
