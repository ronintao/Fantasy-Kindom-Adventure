using System.Collections.Generic;
using UnityEngine;

namespace PathologicalGames {
    public partial class PrefabPool
    {
        int RemoveAllNullDespawnedInstances()
        {
            int nullInstanceCount = 0;

            for (int i = _despawned.Count - 1; i >= 0; i-- )
            {
                if (_despawned[i] == null)
                {
                    _despawned.RemoveAt(i);
                    nullInstanceCount++;
                }
            }

            if (nullInstanceCount > 0)
            {
                string message = string.Format("SpawnPool {0} ({1}): RemoveAllNullDespawnedInstances() found {2} NULL despawned instances!",
                                      this.spawnPool.poolName,
                                      this.prefab.name,
                                      nullInstanceCount);

                Debug.LogError(message);
            }

            return nullInstanceCount;
        }
        public int RemoveAllNullSpawnedInstances()
        {
            // this._spawned
            int nullInstanceCountOfSpawnedList = 0;
            for (int i = this._spawned.Count - 1; i >= 0; i--)
            {
                if (_spawned[i] == null)
                {
                    _spawned.RemoveAt(i);
                    nullInstanceCountOfSpawnedList++;
                }
            }

            // this.spawnPool
            int nullInstanceCountOfSpawnPoolSpawnedList = 0;
            for (int i = this.spawnPool._spawned.Count - 1; i >= 0; i--)
            {
                if (this.spawnPool._spawned[i] == null)
                {
                    this.spawnPool._spawned.RemoveAt(i);
                    nullInstanceCountOfSpawnPoolSpawnedList++;
                }
            }

            if (nullInstanceCountOfSpawnedList > 0)
            {
                string message = string.Format("SpawnPool {0} ({1}): RemoveAllNullSpawnedInstances() found {2} NULL inst in SpawnedList, {3} NULL inst in SpawnPoolList!",
                                      this.spawnPool.poolName,
                                      this.prefab.name,
                                      nullInstanceCountOfSpawnedList, nullInstanceCountOfSpawnPoolSpawnedList);

                Debug.LogError(message);
            }

            return nullInstanceCountOfSpawnedList;
        }

        public void RestorePreloadCount()
        {
            if (_despawned.Count + _spawned.Count > preloadAmount)
            {
                int desired = preloadAmount - _spawned.Count;
                if (desired < 0)
                {
                    desired = 0;
                }
                while (_despawned.Count > desired)
                {
                    Transform inst = this._despawned[0];
                    this._despawned.RemoveAt(0);
                    MonoBehaviour.Destroy(inst.gameObject);
                }
            }
        }

        internal bool DismissInstance(Transform xform)
        {
            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0} ({1}): Dismissing '{2}'",
                                       this.spawnPool.poolName,
                                       this.prefab.name,
                                       xform.name));

            this._spawned.Remove(xform);

            return true;
        }
    }


    public partial class SpawnPool : MonoBehaviour, IList<Transform>
    {
        /// <summary>
        /// Some pooled object doesn't implement OnDespawn() or Reset(), so it cannot be returned to pool.
        /// In this case, that is you wanna just get it from pool but not return it to pool, this function should be called to tell the pool 
        /// about this and the pool should remove this instance from all its instance-lists
        /// </summary>
        /// <param name="instance"></param>
        public void Dismiss(Transform instance)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i]._spawned.Contains(instance))
                {
                    this._prefabPools[i].DismissInstance(instance);
                    break;
                }  
                // Protection - Already despawned?
                else if (this._prefabPools[i]._despawned.Contains(instance))
                {
                    this._prefabPools[i]._despawned.Remove(instance);
                    return;
                }
            }

            // Remove from the internal list. Only active instances are kept.
            //   This isn't needed for Pool functionality. It is just done
            //   as a user-friendly feature which has been needed before.
            this._spawned.Remove(instance);
        }
    }
}