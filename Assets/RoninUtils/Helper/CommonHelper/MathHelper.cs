using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoninUtils.Helper {


    public class MathHelper {

        /// <summary>
        /// 找到绝对值最小的一个
        /// </summary>
        public static float MinAbs(params float[] array) {
            float min = array[0];
            array.ValueForeach( v => min = Math.Abs(v) < Math.Abs(min) ? v : min );
            return min;
        }

        /// <summary>
        /// 找到绝对值最大的一个
        /// </summary>
        public static float MaxAbs(params float[] array) {
            float max = array[0];
            array.ValueForeach(v => max = Math.Abs(v) > Math.Abs(max) ? v : max);
            return max;
        }


    }
}
