using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.RoninFramework {

    /// <summary>
    /// 仿照 CF 的 AssetManager，负责 prefab 的加载
    /// 
    /// 这里有一点需要注意的是，加载使用的是正常的 Resource.Load，而卸载接口只会清除引用，需要调用 UnLoadUnusedAssets 来实际卸载 prefab
    /// </summary>
    public class PrefabService : GameService {

        #region SubCalss Define

        /// <summary>
        /// 异步加载 Prefab 的回调函数
        /// </summary>
        public delegate void OnPrefabAsyncLoaded (GameObject prefab, string id);


        /// <summary>
        /// 异步加载 Prefab 的 Request Priority 设置
        /// </summary>
        public enum EAsyncLoadPriority {
            Trivial       = 0,
            Default       = 100,
            Important     = 200,
            VeryImportant = 350,
            Crucial       = 400,
        }


        /**
         * 内部类，用来保存异步加载的信息
         */
        private class AsyncLoaderRequest {
            public string id;
            public ResourceRequest request;
            public OnPrefabAsyncLoaded callback;

            public AsyncLoaderRequest(string _id, ResourceRequest _request, OnPrefabAsyncLoaded _callback) {
                id = _id;
                request = _request;
                callback = _callback;
            }
        }

        #endregion


        #region Fields


        /**
         * 已经加载的 Prefab ，这里持有一份引用，防止在 UnLoadUnusedAssets 时销毁
         */
        private Dictionary<string, GameObject> mLoadedPrefabs = new Dictionary<string, GameObject>();


        /**
         * 保存异步加载信息的数组，key 是 prefab 路径， value 是 request 信息
         */
        private Dictionary<string, AsyncLoaderRequest> mAsyncLoadRequests = new Dictionary<string, AsyncLoaderRequest>();


        #endregion


        #region Open API

        /// <summary>
        /// 同步加载
        /// </summary>
        public GameObject LoadAsset(string path) {
            // 这里有个问题是，是否需要建立缓存，对已经加载的 prefab 直接从缓存读取而不再使用 load ?
            // 实际测试的结果是：多次加载同一个 prefab 只有第一次有消耗（时间和内存），之后就不再有消耗了，所以unity帮我们已经做了缓存的工作
            GameObject gameObj = Resources.Load<GameObject>(path);
            AddToLoadedListIfNeed(path, gameObj);
            return gameObj;
        }

        /// <summary>
        /// 异步加载，当加载完成会调用 callback
        /// </summary>
        public void LoadAssetAync (string path, OnPrefabAsyncLoaded callback, EAsyncLoadPriority priority = EAsyncLoadPriority.Default) {
            // Already in the request list
            if (mAsyncLoadRequests.ContainsKey(path)) {
                mAsyncLoadRequests[path].callback += callback;
                return;

            // A new asset request
            } else {
                ResourceRequest request = Resources.LoadAsync(path);
                request.priority = (int)priority;
                mAsyncLoadRequests.Add(path, new AsyncLoaderRequest(path, request, callback));
            }
        }

        /// <summary>
        /// 卸载 prefab ，如果 clearRefOnly 则只会清除引用，只有再调用 CleanUnuseAsset 才会真正卸载
        /// </summary>
        public void UnloadAsset(string path, bool clearRefOnly = true) {
            if ( !clearRefOnly ) {
                GameObject obj = GetFromLoadedList(path);
                if (obj != null) Resources.UnloadAsset(obj);
            }

            RemoveFromLoadedList(path);
        }


        /// <summary>
        /// 卸载所有不需要用的 prefab （注意这里实际上连同其它的无引用资源都会卸载）
        /// </summary>
        public void CleanUnuseAsset() {
            Resources.UnloadUnusedAssets();
        }

        #endregion


        #region Life Circle

        public override void Update () {
            base.Update();

            CheckFinishedAsyncLoadPrefab();
        }

        #endregion


        #region Internal Implement

        private void CheckFinishedAsyncLoadPrefab() {
            if (mAsyncLoadRequests.Count == 0)
                return;

            List<string> completeRequests = TmpValueHelper.GetTmpStringList();
            mAsyncLoadRequests.ValueForeach(
                req => {
                    if (req.request.isDone) {
                        AddToLoadedListIfNeed(req.id, req.request.asset as GameObject);
                        req.callback(req.request.asset as GameObject, req.id);
                        completeRequests.Add(req.id);
                    }
                });
            completeRequests.ValueForEach(id => mAsyncLoadRequests.Remove(id));
        }


        private void AddToLoadedListIfNeed(string id, GameObject obj) {
            if ( !mLoadedPrefabs.ContainsKey(id) )
                mLoadedPrefabs.Add(id, obj);
        }

        private GameObject GetFromLoadedList(string id) {
            return mLoadedPrefabs.GetValueSafe(id, null);
        }

        private void RemoveFromLoadedList(string id) {
            if ( mLoadedPrefabs.ContainsKey(id) )
                mLoadedPrefabs.Remove(id);
        }

        #endregion
    }
}
