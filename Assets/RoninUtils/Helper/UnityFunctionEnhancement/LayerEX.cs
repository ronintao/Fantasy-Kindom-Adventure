using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {


    /// <summary>
    /// 这里对 Unity 中使用的 Layer 做一些说明:
    /// 1. gameobject.layer : 对应 LayerIndex 取值范围 [0..31]
    /// 2. 
    /// </summary>
    public class LayerDefine {

        public readonly string    name;
        public readonly int       layerIndex;
        public readonly LayerMask layerMask;

        private LayerDefine (string name, int layerIndex) {
            this.name      = name;
            this.layerIndex     = layerIndex;
            this.layerMask = this.layerIndex >= 0 ? 1 << this.layerIndex : -1;
        }

        public bool IsLayer(MonoBehaviour behaviour) {
            return IsLayer(behaviour.gameObject);
        }

        public bool IsLayer(GameObject gameObject) {
            return gameObject.layer == layerIndex;
        }



        /**
         * Pre Defined Layer
         */

        // Not in Use
        public static readonly LayerDefine None          = new LayerDefine("None",          -1);

        // Unity Defined
        public static readonly LayerDefine Default       = new LayerDefine("Default",        0);
        public static readonly LayerDefine TransparentFX = new LayerDefine("TransparentFX",  1);
        public static readonly LayerDefine IgnoreRaycast = new LayerDefine("Ignore Raycast", 2);
        public static readonly LayerDefine Water         = new LayerDefine("Water",          4);
        public static readonly LayerDefine UI            = new LayerDefine("UI",             5);

        // Ronin Defined
        public static readonly LayerDefine Player        = new LayerDefine("Player",         8);
        public static readonly LayerDefine Friend        = new LayerDefine("Friend",         9);
        public static readonly LayerDefine Enemy         = new LayerDefine("Enemy",         10);
        public static readonly LayerDefine Boss          = new LayerDefine("Boss",          11);

        public static readonly LayerDefine SceneCollideA = new LayerDefine("SceneCollideA", 13);
        public static readonly LayerDefine SceneCollideB = new LayerDefine("SceneCollideB", 14);
        public static readonly LayerDefine SceneTriggerA = new LayerDefine("SceneTriggerA", 15);
        public static readonly LayerDefine SceneTriggerB = new LayerDefine("SceneTriggerB", 16);



        /**
         * Collection
         */

        public static readonly LayerDefine [] CustomLayers = {
            Player,        Friend,        Enemy,         Boss,
            SceneCollideA, SceneCollideB, SceneTriggerA, SceneTriggerB
        };

    }



}
