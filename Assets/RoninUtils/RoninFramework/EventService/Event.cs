using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoninUtils.RoninFramework {

    class Event {

        public Type eventType;
        public int  eventID;

        public Event (Type _eventType, int _eventID) {
            eventType = _eventType;
            eventID = _eventID;
        }


        public override bool Equals (object obj) {
            Event other = obj as Event;
            if (other == null)
                return false;
            return other.eventType == eventType && other.eventID == eventID;
        }

        public override int GetHashCode () {
            return string.Format("{0}___{1}", eventType.ToString(), eventID).GetHashCode();
        }

    }



    class PendingFireEvent {



    }
}
