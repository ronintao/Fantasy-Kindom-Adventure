using RoninUtils.Helper;
using RoninUtils.ResouceIndex.Internal;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RoninUtils.ResouceIndex {
    public class ResouceIndexMenu : RoninScriptableWizard<ResouceIndexMenu> {

        public TextAsset csvFile;

        [MenuItem("RoninUtils/ResouceIndex Generator")]
        public static void GenerateIndexFile () {
            ShowMenu("Create Index File", "Create", "Cancel");
        }

        protected override void OnWizardCreate () {
            base.OnWizardCreate();
            Dictionary<string, string> [] data = CSVReader.ParseWithTag(csvFile.text.Trim());
            ResouceIndexGenerator.GenerateFile(csvFile.name, data);
        }

    }
}


