using System;
using System.Collections.Generic;

namespace RoninUtils.Helper {
    public static class ArrayExtend {

        #region Useful Function

        /// <summary>
        /// 生成 count 个互不相同的，范围在[min, max)之间的随机值，目前只支持 int 类型
        /// 算法见 http://blog.sina.com.cn/s/blog_57de62c00100ltak.html
        /// </summary>
        public static int[] generateDifferntRandomNumber (int count, int min, int max) {
            int [] array  = new int[max - min];
            for (int i = 0; i < max - min; i++)
                array[i] = i + min;
            for (int i = max - min - 1; i > 0; i--)
                swap(array, i, UnityEngine.Random.Range(0, i));

            int [] result = new int[count];
            System.Array.Copy(array, result, count);
            return result;
        }


        #endregion


        #region Normal Array Enhancement

        /// <summary>
        /// 判断数组是否为空
        /// 即使数组为空，这个方式也不会出错
        /// </summary>
        public static bool IsNullOrEmpty<T> (this T[] array) {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// 获取数组的第 index 个元素，如果超过数组长度或者该项为空，则返回 defaultValue
        /// </summary>
        public static T SafeIndex<T> (this T[] array, int index, T defaultValue = default(T)) {
            if (array == null || array.Length <= index || array[index] == null)
                return defaultValue;
            return array[index];
        }

        /// <summary>
        /// For Each
        /// </summary>
        public static void ValueForeach<TValue> (this TValue[] array, Action<TValue> action) {
            if (array == null)
                return;

            for (int i = 0; i < array.Length; i++) {
                action(array[i]);
            }
        }

        /// <summary>
        /// 交换数组里的 indexA 和 indexB 元素，该方法没有对数组进行非空检查
        /// </summary>
        public static void swap<T> (this T[] array, int indexA, int indexB) {
            T tmp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = tmp;
        }


        /// <summary>
        /// 包含该元素
        /// </summary>
        public static bool Contains<T> (this T[] array, T value) {
            if (array == null)
                return false;

            return Array.Exists<T>(array, item => item.Equals(value));
        }



        #endregion


        #region Dictionary Enhancement


        public static void Set<TKey, TValue> (this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }




        /// <summary>
        /// 遍历字典的值，似乎没有特别好的办法规避 foreach
        /// </summary>
        public static void ValueForeach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<TValue> action) {
            foreach (var value in dictionary.Values) {
                action(value);
            }
        }

        public static TValue GetValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue)) {
            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }


        public static TValue GetValueOrNew<TKey, TValue> (this Dictionary<TKey, TValue> dictionary, TKey key) where TValue : new() {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, new TValue());

            return dictionary[key];
        }


        public static void AddOrExecute<TKey, TValue> (this Dictionary<TKey, TValue> dictionary, TKey key, TValue value, Action<TValue> action) {
            if ( !dictionary.ContainsKey(key) )
                dictionary.Add(key, value);
            else
                action(dictionary[key]);
        }



        #endregion


        #region List Enhancement

        /// <summary>
        /// 添加不重复的元素
        /// </summary>
        public static void AddIfUnRepeat<TValue>(this List<TValue> list, TValue value) {
            if ( list.Contains(value) )
                return;

            list.Add(value);
        }


        /// <summary>
        /// 从数组转化为列表，如果设置了 selectFunc ，则只会添加其中符合要求的元素； 如果设置了 compareFunc ，则会返回一个排序后的列表
        /// </summary>
        public static List<TValue> ParseArray<TValue> (TValue [] array, Judge<TValue> selectFunc = null, Comparison<TValue> compareFunc = null) {
            List<TValue> list = new List<TValue>();

            if (array.IsNullOrEmpty())
                return list;

            if (compareFunc != null)
                Array.Sort(array, compareFunc);

            for (int i = 0; i < array.Length; i ++) {
                if (selectFunc != null && selectFunc(array[i]))
                    list.Add(array[i]);
            }

            return list;
        }


        /// <summary>
        /// 遍历列表
        /// </summary>
        public static void ValueForEach<TValue>(this List<TValue> list, Action<TValue> action) {
            if (list == null)
                return;

            for (int i = 0; i < list.Count; i ++) {
                action(list[i]);
            }
        }

        /// <summary>
        /// 处理列表，并移除部分元素 (需要注意的是，这个列表是从后向前处理的)
        /// </summary>
        /// <param name="processFunc">如果return true，则该元素将被移除</param>
        public static void ProcessAndRemove<TValue>(this List<TValue> list, Judge<TValue> processFunc) {
            int listCount = list.Count;
            for (int i = listCount - 1; i >= 0; i --) {
                if ( processFunc(list[i]) )
                    list.RemoveAt(i);
            }
        }


        #endregion
    }
}
