using System;

namespace RoninUtils.Helper {
    public static class ThreadAndLockHelper {

        #region Lock Syntactic Sugar

        public static T CreateSafe<T>(T instance, object lockObject) {
            lock (lockObject) {
                if (instance == null)
                    return default(T);
                else
                    return instance;
            }
        }


        #endregion
    }
}
