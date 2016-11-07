using RoninUtils.Helper;
using RoninUtils.ResouceIndex.Internal;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RoninUtils.ResouceIndex {

    public class ResouceIndexProcesser : AssetPostprocessor {

        /// <summary>
        /// 当导入资源，或资源发生修改时，都会自动调用该方法
        /// </summary>
        static void OnPostprocessAllAssets (
                string[] importedAssets,
                string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths) {

            if (!ResouceIndexConfig.AUTO_GENERATE || importedAssets == null || importedAssets.Length == 0)
                return;

            for (int i = 0; i < importedAssets.Length; i ++) {
                string assetPath = importedAssets[i];

                // 如果不是放在指定的文件夹下，不处理
                if ( !assetPath.StartsWith(ResouceIndexConfig.CSV_FOLDER_PATH) )
                    continue;

                // 如果不是 csv 文件，不处理
                if ( !assetPath.EndsWith(".csv") )
                    continue;

                // 如果无法加载，不处理
                TextAsset textAsset = Resources.Load<TextAsset>(assetPath);
                if (textAsset == null)
                    continue;

                Dictionary<string, string> [] data = CSVReader.ParseWithTag(textAsset.text.Trim());
                ResouceIndexGenerator.GenerateFile(textAsset.name, data);

                Resources.UnloadAsset(textAsset);
            }
        }
    }

}
