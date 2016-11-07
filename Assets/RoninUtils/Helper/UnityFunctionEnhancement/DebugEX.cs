using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {
    public static class DebugEX {

        public static void LogFunction() {
            UnityEngine.Debug.Log(new StackFrame(1, true).GetMethod().Name);
        }


    }
}
