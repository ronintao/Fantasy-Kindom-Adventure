using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RoninUtils.RoninFramework {

    public class SystemConfigFile : BaseConfigData {

        private static Assembly BaseConfigDataAssemly = typeof(BaseConfigData).Assembly;

        private const string FIELD_NAME___FILE_PATH  = "FilePath";
        private const string FIELD_NAME___CLASS_NAME = "ClassName";
        private const string FIELD_NAME___INIT_FIRST = "InitFirst";


        /// <summary>
        /// 配置文件路径, 应当配置为相对路径，且不包含 assets ， 另外需要注意放在 Resources 下，保证会被打包
        /// </summary>
        public string filePath { get; private set; }

        /// <summary>
        /// 配置类的类名, 应当包含 namespace，如 TestNameSpace.TestClass
        /// </summary>
        public string className { get; private set; }

        /// <summary>
        /// 是否在启动时加载
        /// </summary>
        public bool initFirst { get; private set; }

        public override string GetPrimaryKeyValue () {
            return className;
        }

        public override void ParseData (long index, Dictionary<string, string> values) {
            className = ReadString (FIELD_NAME___CLASS_NAME, values);
            filePath  = ReadString (FIELD_NAME___FILE_PATH,  values);
            initFirst = ReadBoolean(FIELD_NAME___INIT_FIRST, values);
        }

        public Type GetClassType() {
            return BaseConfigDataAssemly.GetType(className);
        }

    }
}
