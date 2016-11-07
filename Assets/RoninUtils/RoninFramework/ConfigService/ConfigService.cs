using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoninUtils.Helper;

namespace RoninUtils.RoninFramework {

    /**
     * 具体数据项的保存集合
     */
    class ParsedConfig {

        /**
         * 实际上必须是 Dictionary<string, T extends BaseConfigData>
         * 但是由于字典 ConfigService.mConfigs 中保存的元素必须指出确定的类型而不能再加泛型，所以这里使用 object 来持有
         */
        private object datas;

        public ParsedConfig(object datas) {
            this.datas = datas;
        }

        public Dictionary<string, T> GetDatas<T>() where T : BaseConfigData {
            return datas as Dictionary<string, T>;
        }

        public T GetData<T>(string key) where T : BaseConfigData {
            return (datas as Dictionary<string, T>).GetValueSafe(key);
        }
    }


    /// <summary>
    /// 管理所有 Csv Config 文件
    /// </summary>
    public class ConfigService : GameService {


        /**
         * 配置文件的配置
         */
        private ParsedConfig mConfigFileCfg;


        /**
         * 保存所有已经解析过的配置
         */
        private Dictionary<Type, ParsedConfig> mConfigs = new Dictionary<Type, ParsedConfig>();


        /**
         * 初始化，call by GameFramework
         * 会加载 mConfigFileCfg ， 并加载其中标记为 initFirst 的配置
         */
        public override void Init () {
            base.Init();
            mConfigFileCfg = new ParsedConfig( ReadCSVData<SystemConfigFile>(ConfigServieCfg.CONFIG_FILES) );
        }


        #region Open API

        /// <summary>
        /// 获取对应的配置
        /// </summary>
        public Dictionary<string, T> GetConfig<T> () where T : BaseConfigData {
            Type type = typeof(T);
            ParsedConfig config = mConfigs.ContainsKey(type) ? mConfigs[type] : ParseConfigFile<T>();
            return config.GetDatas<T>();
        }


        #endregion


        #region Internal : Pase Config

        /**
         * 解析 SystemConfigFile 根据映射关系加载对应的配置
         */
        private ParsedConfig ParseConfigFile<T> () where T : BaseConfigData {
            Type type = typeof(T);

            SystemConfigFile fileConfig = mConfigFileCfg.GetData<SystemConfigFile>(type.FullName);

            ParsedConfig parsedConfig = new ParsedConfig( ReadCSVData<T>(fileConfig.filePath) );
            mConfigs.Add(type, parsedConfig);

            return parsedConfig;
        }

        /**
         * 读取 CSV 获取其中配置的数据
         */
        private Dictionary<string, T> ReadCSVData<T> (string relativePath) where T : BaseConfigData {
            // Get Raw Data
            string text = FileHelper.ReadFileRelativePath(relativePath);
            Dictionary<string, string> [] csvLines = CSVReader.ParseWithTag(text);

            // Parse Raw Data
            Dictionary<string, T> dataList = new Dictionary<string, T>();
            for (int i = 0; i < csvLines.Length; i ++) {
                T data = Activator.CreateInstance(typeof(T)) as T;
                data.ParseData(i, csvLines[i]);
                dataList.Add(data.GetPrimaryKeyValue(), data);
            }

            return dataList;
        }

        #endregion

    }
}
