using UnityEditor;
using UnityEngine;

namespace RoninUtils.Helper.ProjectStartUp {

    /// <summary>
    /// 添加 LayerDefine 中的自定义 layer
    /// </summary>
    public static class LayerStartUp {


        public static void Execute() {

            // 通过反射，找到 tagManager.layers
            SerializedObject   tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            // 添加 Layer
            foreach (LayerDefine layer in LayerDefine.CustomLayers) {
                SerializedProperty layerProperty = layersProp.GetArrayElementAtIndex(layer.layerIndex);
                if (layerProperty.stringValue != layer.name) {
                    layerProperty.stringValue = layer.name;
                    Debug.Log("Add Layer " + layer.name);
                }
            }

            tagManager.ApplyModifiedProperties();
        }

    }
}
