using UnityEngine;
using System.Collections;

namespace RoninUtils.Helper {

    /// <summary>
    /// 单例类，静态初始化
    /// </summary>
    public class RoninSingleton<T> where T : RoninSingleton<T> {

        protected static T mInstance = default(T);

        public static T Instance { get { return mInstance; } }

    }


    /// <summary>
    /// 单例类，在调用 Instance 的时候初始化
    /// </summary>
    public class RoninLazySingleton<T> where T : RoninSingleton<T> {

        protected volatile static T      mInstance;
        private   readonly static object mInstanceLock = new object();

        public static T Instance {
            get {
                if (mInstance == null) {
                    mInstance = ThreadAndLockHelper.CreateSafe(mInstance, mInstanceLock);
                }
                return mInstance;
            }
        }
    }


    /// <summary>
    /// 单例类，在 Awake 的时候初始化
    /// </summary>
    public class RoninSingletonComponent<T> : RoninComponent
            where T : RoninSingletonComponent<T> {

        public static T Instance { get; protected set; }

        protected override void Awake() {
            base.Awake();
            Instance = this as T;
        }

    }
}


