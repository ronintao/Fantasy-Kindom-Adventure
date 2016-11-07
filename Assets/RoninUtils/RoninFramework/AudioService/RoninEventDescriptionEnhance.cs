using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD;
using FMOD.Studio;

namespace RoninUtils.RoninFramework {
    public static class RoninEventDescriptionEnhance {

        public static bool isOneShotSimple(this EventDescription eventDescription, string eventName) {
            // snap shot is a parameter snap shot, not for play
            if (eventName == null || eventName.StartsWith("snapshot", StringComparison.CurrentCultureIgnoreCase))
                return false;

            bool isOneShot = false;
            eventDescription.isOneshot(out isOneShot);
            return isOneShot;
        }


        public static bool is3DSimple(this EventDescription eventDescription) {
            bool is3D = false;
            eventDescription.is3D(out is3D);
            return is3D;
        }


    }
}
