using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace RoninUtils.ResouceIndex.Internal {
    class ResouceIndexGenerator {

        private const string FILE_HEAD =
@"namespace RoninUtils.ResouceIndex {
    public partial class R {
        public static class {0} {
";

        private const string FILE_CONTENT = 
"          public const string {0} = \"{1}\"; ";

        private const string FILE_CONTENT_INT =
"          public const string {0} = {1}; ";


        private const string FILE_END = 
@"
        } // end of subClass
    }   // end of class R
}   // end of namespace
";


        public static void GenerateFile(string csvFileName, Dictionary<string, string>[] data) {
            bool isIntValue = csvFileName.StartsWith(ResouceIndexConfig.CSV_NAME_INT_VALUE);

            // 清除标识符
            csvFileName = csvFileName.Replace(ResouceIndexConfig.CSV_NAME_INT_VALUE, "");

            string relativePath = GetRelativePath(csvFileName);
            string fileFullName = GetFileFullPath(csvFileName);

            // 清空原有的文件
            File.Create(fileFullName).Close();

            // 填充新的内容
            StreamWriter stream = new StreamWriter(fileFullName);

            stream.WriteLine(FILE_HEAD.Replace("{0}", csvFileName));
            for (int i = 0; i < data.Length; i ++) {
                if (isIntValue)
                    stream.WriteLine(string.Format(FILE_CONTENT_INT, data[i][ResouceIndexConfig.CSV_KEY], int.Parse(data[i][ResouceIndexConfig.CSV_VALUE])));
                else
                    stream.WriteLine(string.Format(FILE_CONTENT,     data[i][ResouceIndexConfig.CSV_KEY], data[i][ResouceIndexConfig.CSV_VALUE]));
            }
            stream.WriteLine(FILE_END);

            stream.Close();

            // 将该文件导入 Unity 工程
            AssetDatabase.ImportAsset(relativePath);
        }


        /**
         * AssetDatabase 或是 Resources 要处理资源，需要一个相对路径，如 Assets/Ronin.txt
         */
        private static string GetRelativePath(string csvFileName) {
            return "Assets/" + ResouceIndexConfig.CODE_FOLDER_PATH + "/" + string.Format("ResouceIndex_{0}.cs", csvFileName);
        }


        /**
         * 使用 File 处理文件，需要给定完整路径，如 D:/HelloWorld/Assets/Ronin.txt
         */
        private static string GetFileFullPath(string csvFileName) {
            return Application.dataPath + ResouceIndexConfig.CODE_FOLDER_PATH + "/" + string.Format("ResouceIndex_{0}.cs", csvFileName);
        }

    }
}
