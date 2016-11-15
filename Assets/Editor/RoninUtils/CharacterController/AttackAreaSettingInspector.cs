using UnityEngine;
using System.Collections;
using UnityEditor;


namespace RoninUtils.RoninCharacterController {

    [CustomEditor(typeof(AttackAreaSetting))]
    public class AttackAreaSettingInspector : Editor {

        private AttackAreaSetting mTargetSetting;

        private void OnEnable() {
            mTargetSetting = target as AttackAreaSetting;
        }

        public override void OnInspectorGUI () {
            base.OnInspectorGUI();

            if (GUILayout.Button("Get All Item In Child")) {
                mTargetSetting.attackItems = GetAllItemInChild();
                serializedObject.ApplyModifiedProperties();
            }
        }


        private AttackAreaItem [] GetAllItemInChild() {
            return mTargetSetting.GetComponentsInChildren<AttackAreaItem>();
        }
    }




}

