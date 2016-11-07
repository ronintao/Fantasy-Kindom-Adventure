using RoninUtils.Helper;
using System.Collections.Generic;


namespace RoninUtils.RoninFramework {
    public abstract class BaseConfigData {

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="index">当前条目序号</param>
        /// <param name="values">当前条目属性值map，key是head tag，value是对应的值</param>
        public abstract void ParseData (long index, Dictionary<string, string> values);

        /// <summary>
        /// 提供主键的值
        /// </summary>
        public abstract string GetPrimaryKeyValue ();


        #region Parse Data To Specific Type


        /// <summary>
        /// 读取 float Field
        /// </summary>
        protected static float ReadFloat (string fieldName, Dictionary<string, string> values, float defaultValue = 0f) {
            string rawData = values.GetValueSafe(fieldName);
            return rawData.ToFloat(defaultValue);
        }


        /// <summary>
        /// 读取 hex int Field 如 0xFF11 或是 FF11
        /// </summary>
        protected static int ReadHexInt32 (string fieldName, Dictionary<string, string> values, int defaultValue = 0) {
            string rawData = values.GetValueSafe(fieldName);
            return rawData.HexToInt(defaultValue);
        }


        /// <summary>
        /// 读取 int field
        /// </summary>
        protected static int ReadInt32 (string fieldName, Dictionary<string, string> values, int defaultValue = 0) {
            string rawData = values.GetValueSafe(fieldName);
            return rawData.ToInt(defaultValue);
        }


        /// <summary>
        /// 读取 unsigned long field
        /// </summary>
        protected static ulong ReadULong (string fieldName, Dictionary<string, string> values, ulong defaultValue = 0) {
            string rawData = values.GetValueSafe(fieldName);
            return rawData.ToULong(defaultValue);
        }


        /// <summary>
        /// 读取 string Field ，如果是 null 或 “”，返回 defaultValue
        /// </summary>
        protected static string ReadString (string fieldName, Dictionary<string, string> values, string defaultValue = null) {
            string rawData = values.GetValueSafe(fieldName);
            return string.IsNullOrEmpty(rawData) ? defaultValue : rawData;
        }


        /// <summary>
        /// 读取 bool field  可以正常解析的只有 true false (大小写不敏感)
        /// </summary>
        protected static bool ReadBoolean (string fieldName, Dictionary<string, string> values, bool defaultValue = false) {
            string rawData = values.GetValueSafe(fieldName);
            return rawData.ToBool(defaultValue);
        }

        #endregion

    }

}
