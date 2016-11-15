using UnityEditor;

namespace RoninUtils.Helper {

    public abstract class RoninScriptableWizard<T> : ScriptableWizard
         where T : RoninScriptableWizard<T> {

        protected static T m_Instance;

        protected static void ShowMenu(string title, string createLabel, string cancelLabel) {
            if (m_Instance == null)
                m_Instance = ScriptableWizard.DisplayWizard<T>(title, createLabel, cancelLabel);

            m_Instance.Init();
            m_Instance.Show();
        }


        protected virtual void Init() {
        }


        /// <summary>
        /// Call When Create Button Clicked
        /// </summary>
        protected virtual void OnWizardCreate() {
        }


        /// <summary>
        /// Call When Update
        /// </summary>
        protected virtual void OnWizardUpdate () {
        }


        /// <summary>
        /// Call When Cancel Button Clicked
        /// </summary>
        protected virtual void OnWizardOtherButton () {
            this.Close();
        }
    }

}