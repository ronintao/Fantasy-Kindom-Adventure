using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathologicalGames;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.RoninFramework {

    public class PoolService : GameService {

        #region Helper Class : DestroySchedule

        protected struct DestroySchedule {
            public SpawnPool Pool;
            public float ScheduleTime;
            public GameObject Object;

            public DestroySchedule(SpawnPool pool, GameObject obj, float time) {
                Pool = pool;
                Object = obj;
                ScheduleTime = time;
            }

        }

        #endregion

        private List<DestroySchedule> mDestroySchedule = new List<DestroySchedule>();

        public override void Update () {
            base.Update();
            DespawnSchedule();
        }


        #region Create / Destroy SpawnPool

        /// <summary>
        /// 创建一个缓存池
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="destroyOnLoad"></param>
        /// <param name="broadCastEventMessage"></param>
        /// <returns></returns>
        public SpawnPool CreatePool(string poolName, bool destroyOnLoad = true, bool broadCastEventMessage = true) {
            SpawnPool pool = PoolManager.Pools.Create(poolName);
            pool.dontDestroyOnLoad = !destroyOnLoad;
            pool.ShouldBroadCastMessage = broadCastEventMessage;

            return pool;
        }

        /// <summary>
        /// 销毁一个缓存池
        /// </summary>
        public void DestroyPool(string poolName) {
            PoolManager.Pools.Destroy(poolName);
        }


        #endregion



        #region Create / Destroy PrefabPool


        /// <summary>
        /// 缓存池预加载一个 Prefab
        /// </summary>
        /// <param name="poolName">缓存池名称</param>
        /// <param name="preloadCount">预加载的实例个数</param>
        /// <param name="limitCount">缓存池实例限制</param>
        public void PreloadPrefab(string poolName, GameObject prefab, int preloadCount, int limitCount) {
            PreloadPrefab(PoolManager.Pools[poolName], prefab, preloadCount, limitCount);
        }


        /// <summary>
        /// 缓存池预加载一个 Prefab
        /// </summary>
        /// <param name="pool">缓存池</param>
        /// <param name="preloadCount">预加载的实例个数</param>
        /// <param name="limitCount">缓存池实例限制</param>
        public void PreloadPrefab (SpawnPool pool, GameObject prefab, int preloadCount, int limitCount) {
            if (pool.GetPrefabPool(prefab.transform) != null) {
                Debug.LogWarning("Create A Exist Prefab Pool");
                return;
            }

            PrefabPool prefabPool = new PrefabPool(prefab.transform);

            if (limitCount > 0) {
                preloadCount = Mathf.Min(preloadCount, limitCount);
                prefabPool.limitAmount = limitCount;
                prefabPool.preloadAmount = preloadCount;
                prefabPool.limitInstances = true;

            } else {
                prefabPool.preloadAmount = preloadCount;
                prefabPool.limitInstances = false;
            }

            pool.CreatePrefabPool(prefabPool);
        }


        /// <summary>
        /// 将缓存池中的不用的 Prefab Pool 清空
        /// </summary>
        public void ClearPool (string poolName) {
            ClearPool( PoolManager.Pools[poolName] );
        }


        /// <summary>
        /// 将缓存池中的不用的 Prefab Pool 清空
        /// </summary>
        public void ClearPool (SpawnPool pool) {
            pool.DestroyAllUnUsedPrefabPool();
        }

        #endregion



        #region Creat / Destroy Instance


        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <param name="parent">生成的instance的parent</param>
        /// <param name="lifeTime">如果设置的大于0，则会在一定时间后自动Despawn</param>
        public GameObject Spawn (string poolName, GameObject prefab, GameObject parent, float lifeTime = 0) {
            return Spawn(poolName, prefab, Vector3.zero, Quaternion.identity, parent, lifeTime);
        }


        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <param name="parent">生成的instance的parent</param>
        /// <param name="lifeTime">如果设置的大于0，则会在一定时间后自动Despawn</param>
        public GameObject Spawn (SpawnPool pool, GameObject prefab, GameObject parent, float lifeTime = 0) {
            return Spawn(pool, prefab, Vector3.zero, Quaternion.identity, parent, lifeTime);
        }

        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <param name="parent">生成的instance的parent</param>
        /// <param name="lifeTime">如果设置的大于0，则会在一定时间后自动Despawn</param>
        public GameObject Spawn (string poolName, GameObject prefab, Vector3 position, Quaternion rotation, GameObject parent, float lifeTime = 0) {
            return Spawn( PoolManager.Pools[poolName], prefab, position, rotation, parent, lifeTime );
        }

        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <param name="parent">生成的instance的parent</param>
        /// <param name="lifeTime">如果设置的大于0，则会在一定时间后自动Despawn</param>
        public GameObject Spawn(SpawnPool pool, GameObject prefab, Vector3 position, Quaternion rotation, GameObject parent, float lifeTime = 0) {
            Transform trans = pool.Spawn(prefab, position, rotation, parent.transform);
            if (lifeTime > 0) {
                Despawn(pool, trans.gameObject, lifeTime);
            }
            return trans.gameObject;
        }


        /// <summary>
        /// 销毁一个实例
        /// </summary>
        /// <param name="delay">在多久之后销毁</param>
        public void Despawn(string poolName, GameObject instance, float delay = 0f) {
            Despawn(PoolManager.Pools[poolName], instance, delay);
        }


        /// <summary>
        /// 销毁一个实例
        /// </summary>
        /// <param name="delay">在多久之后销毁</param>
        public void Despawn(SpawnPool pool, GameObject instance, float delay = 0f) {
            // Despawn Now
            if (delay <= float.Epsilon) {
                pool.Despawn(instance.transform, pool.transform);

            // Despawn Later
            } else {
                mDestroySchedule.Add(new DestroySchedule(pool, instance, Time.time + delay));
            }
        }

        /**
         * 销毁 延迟队列 中的计划任务
         */
        private void DespawnSchedule() {
            mDestroySchedule.ProcessAndRemove(
                schedule => {
                    if (schedule.ScheduleTime <= Time.time) {
                        Despawn(schedule.Pool, schedule.Object);
                        return true;
                    } else {
                        return false;
                    }
                });
        }

        #endregion

    }
}
