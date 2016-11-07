using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(ETCArea))]
public class ETCAreaInspector : Editor {

    private ETCArea.AreaPreset preset = ETCArea.AreaPreset.Choose;

    public override void OnInspectorGUI () {

        ETCArea t = (ETCArea)target;

        t.show = ETCGuiTools.Toggle("Show at runtime", t.show, true);
        EditorGUILayout.Space();

        preset = (ETCArea.AreaPreset)EditorGUILayout.EnumPopup("Preset", preset);
        if (preset != ETCArea.AreaPreset.Choose) {
            t.ApplyPreset(preset);
            preset = ETCArea.AreaPreset.Choose;
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(t);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
