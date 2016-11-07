using UnityEngine;
using System.Collections;
using UnityEditor;
using RoninUtils.Helper;


namespace RoninUtils.Helper.ProjectStartUp {


    [InitializeOnLoad]
    public class ProjectStartUp {

        // Call When Open The Unity Editor
        static ProjectStartUp () {

            LayerStartUp.Execute();

        }

    }


}

