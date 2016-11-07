using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {

    public static class TransformEX {



        /// <summary>
        /// 打回到初始状态
        /// </summary>
        public static void Reset(this Transform transform) {
            transform.localPosition = Vector3.zero;
            transform.localScale    = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }



        /// <summary>
        /// 复制 transfrom 包括 position scale rotation parent(可选)
        /// </summary>
        public static void CopyTo(this Transform from, Transform to, bool setParent = false) {
            if (setParent) {
                to.parent = from.parent;
                to.localPosition = from.localPosition;
                to.localScale    = from.localScale;
                to.localRotation = from.localRotation;
            } else {
                to.position   = from.position;
                to.localScale = to.parent == null ? from.lossyScale : from.lossyScale.Division(to.parent.lossyScale);
                to.rotation   = from.rotation;
            }
        }




        #region Set Parent Enhancement

        /**
         * 
         * 
         */
        public static void SetParent(this GameObject gameObject, GameObject parentObject) {
            gameObject.transform.SetParent(parentObject.transform);
        }

        public static void SetParent(this GameObject gameObject, Component parentComponent) {
            SetParent(gameObject, parentComponent.gameObject);
        }

        public static void SetParent(this Component component, GameObject parentObject) {
            SetParent(component.gameObject, parentObject);
        }

        public static void SetParent(this Component component, Component parentComponent) {
            SetParent(component.gameObject, parentComponent.gameObject);
        }


        #endregion


    }
}
