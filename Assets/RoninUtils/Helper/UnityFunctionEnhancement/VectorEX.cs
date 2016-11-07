using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {

    public static class VectorEnhance {

        /*************************************************
         * 
         * Vector Math Enhancement
         * 
         *************************************************/

        /// <summary>
        /// 各分量的乘积
        /// </summary>
        public static float XYZ(this Vector3 vector) {
            return vector.x * vector.y * vector.z;
        }


        /// <summary>
        /// 各分量除法，注意没有除0保护
        /// </summary>
        public static Vector3 Division(this Vector3 left, Vector3 right) {
            return new Vector3(left.x / right.x, left.y / right.y, left.z / right.z);
        }



        public static Vector3 SetX(this Vector3 vector, float x) {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 SetY (this Vector3 vector, float y) {
            return new Vector3(vector.x, y, vector.z);
        }


        public static Vector3 SetZ (this Vector3 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }

        /*************************************************
         * 
         * Vector Direction Enhancement
         * 
         *************************************************/

        public static bool IsPointUp(this Vector3 vector) {
            return Vector3.Dot(vector, Vector3.up) > 0;
        }
    }





}
