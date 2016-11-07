using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoninUtils.Helper {

    /// <summary>
    /// 在代码中常常需要创建一些临时的变量，如果是在 udpate 等函数中，反复的创建，就会导致 gc 
    /// 所以这里提供一个比较优雅的解决方式，提供一些临时变量
    /// </summary>
    public static class TmpValueHelper {

        /**
         * 临时的 string 列表，在调用 Get 时会清空原值
         */
        private static List<string> tmpStringList = new List<string>(10);
        public static List<string> GetTmpStringList() {
            tmpStringList.Clear();
            return tmpStringList;
        }

        /**
         * 临时的 int 列表，在调用 Get 时会清空原值
         */
        private static List<int> tmpIntList = new List<int>(10);
        public static List<int> GetTmpIntList () {
            tmpIntList.Clear();
            return tmpIntList;
        }


        private static StringBuilder tmpStringBuilder = new StringBuilder();
        public static StringBuilder GetStringBuilder() {
            tmpStringBuilder.Length = 0;
            return tmpStringBuilder;
        }

    }
}
