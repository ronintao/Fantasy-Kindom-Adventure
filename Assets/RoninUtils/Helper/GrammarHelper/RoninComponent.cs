using UnityEngine;

namespace RoninUtils.Helper {

    /// <summary>
    /// Unity 的 Component 的方法没有父类，难以 override ，因此这里提供一个基础的类来暴露方法
    /// 
    /// 另外兼容了 Pool Service
    /// </summary>
    public abstract partial class RoninComponent : MonoBehaviour {

        #region Pool Service

        private bool isActive = false;

        /**
         * Pool Service 会使用 OnSpawned 来代替 Awake，第一次 Spawn 时，由于从 disable -> enable ，会导致 Awake
         * 之后再复用时，就不会 Awake 了，因此这里手动调用 Awake 来保证生命周期一致
         */
        private void OnSpawned() {
            if ( !isActive )
                Awake();
        }

        /**
         * 同理如上
         */
        private void OnDespawned () {
            isActive = false;
            OnDestroy();
        }

        #endregion


        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake () {
            isActive = true;
        }


        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start () { }


        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable () { }


        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// </summary>
        protected virtual void OnDisable () { }


        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update () { }


        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate () { }


        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        protected virtual void OnValidate () { }


        private void OnApplicationPause (bool paused) {
            if (paused)
                OnApplicationPaused();
            else
                OnApplicationResumed();
        }


        /// <summary>
        /// Sent to all game objects when the player pauses.
        /// </summary>
        protected virtual void OnApplicationPaused () { }


        /// <summary>
        /// Sent to all game objects when the player Resumed
        /// </summary>
        protected virtual void OnApplicationResumed () { }


        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected virtual void OnDestroy () { }
    }
}
