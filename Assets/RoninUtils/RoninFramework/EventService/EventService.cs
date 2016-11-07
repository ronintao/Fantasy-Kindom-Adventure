using RoninUtils.Helper;
using System;
using System.Collections.Generic;

namespace RoninUtils.RoninFramework {

    /// <summary>
    /// Event 系统，需要依赖于 Indexer 系统，用 event.csv 来配置所有 event_id
    /// 建议命名为 [INT]Event.csv
    /// 如果需要分类，则建议使用 [INT]Event_Type1.csv  [INT]Event_Type2.csv  [INT]Event_TypeX.csv 多张表来表示，这样会建立多个 EventClass 从而产生分类效果
    /// 
    /// 事件分为两种（仿照Android）
    /// Normal Event ：普通Event，在SendEvent时立刻触发监听者
    /// Sticky Event : 常驻Event，如果当前有监听者，则立刻消费，否则直到有消费者的时候才消费
    /// 
    /// 监听者也分为两种
    /// 一种是监听一类事件，一些service通常需要这样做
    /// 一种是监听这一类事件中的某些id，通常这就足够了
    /// 这里考虑是否增加监听者的优先级，以及是否增加监听者对事件的消费（使得之后的监听者不再收到事件），考虑到可能会使得监听者代码混乱，所以目前不加入这些功能
    /// </summary>
    public class EventService : GameService {

        /// <summary>
        /// 回调格式
        /// </summary>
        public delegate void EventHandlerFunc(Type eventType, int eventID);

        /**
         * 对一类型事件的无差别监听者，key 是事件分类，value 是监听者
         */
        private Dictionary<Type,  EventHandlerFunc> mTypeEventHandlers = new Dictionary<Type,  EventHandlerFunc>();

        /**
         * 对一个事件的无差别监听者，key 是事件分类，value 是监听者
         */
        private Dictionary<Event, EventHandlerFunc> mIDEventHandlers   = new Dictionary<Event, EventHandlerFunc>();

        /**
         * 未消费的 stick 事件列表
         */
        private List<Event> mStickySyncEvents  = new List<Event>();


        #region Register Event

        /// <summary>
        /// 监听一类事件
        /// </summary>
        public void RegisterEvent(Type eventType, EventHandlerFunc callback, bool checkStickEvent = false) {
            mTypeEventHandlers.AddOrExecute(eventType, callback, del => del += callback);

            // Fire All Sticky Event if need
            if (checkStickEvent) {
                List<Event> fireStickEventList = new List<Event>();
                mStickySyncEvents .ValueForEach( stickEvent => {
                    if (stickEvent.eventType == eventType)
                        fireStickEventList.Add(stickEvent); }
                );
                fireStickEventList.ValueForEach( stickEvent => { FireStickEvent(stickEvent, callback); } );
            }
        }


        /// <summary>
        /// 监听一类事件中的某个 id
        /// </summary>
        public void RegisterEvent(Type eventType, int eventID, EventHandlerFunc callback, bool checkStickEvent = false) {
            Event e = new Event(eventType, eventID);
            mIDEventHandlers.AddOrExecute(e, callback, del => del += callback);

            if (checkStickEvent && mStickySyncEvents.Contains(e)) {
                FireStickEvent(e, callback);
            }
        }


        /// <summary>
        /// 监听一类事件中的一些 id
        /// </summary>
        public void RegisterEvent (Type eventType, int[] eventIDs, EventHandlerFunc callback, bool checkStickEvent = false) {
            eventIDs.ValueForeach(eventID => RegisterEvent(eventType, eventID, callback, checkStickEvent));
        }

        #endregion // Register Event



        #region Remove Delegate

        /// <summary>
        /// 从类型监听列表中移除
        /// </summary>
        public void UnRegisterEvent(Type eventType, EventHandlerFunc callback) {
            mTypeEventHandlers[eventType] -= callback;
            if (mTypeEventHandlers[eventType] == null)
                mTypeEventHandlers.Remove(eventType);
        }

        /// <summary>
        /// 从事件监听列表中移除
        /// </summary>
        public void UnRegisterEvent (Type eventType, int eventID, EventHandlerFunc callback) {
            Event e = new Event(eventType, eventID);
            mIDEventHandlers[e] -= callback;
            if (mIDEventHandlers[e] == null)
                mIDEventHandlers.Remove(e);
        }

        /// <summary>
        /// 从多个事件监听列表中移除
        /// </summary>
        public void UnRegisterEvent (Type eventType, int[] eventIDs, EventHandlerFunc callback) {
            eventIDs.ValueForeach(eventID => UnRegisterEvent(eventType, eventID, callback));
        }

        #endregion // Remove Delegate



        #region Send Event

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent (Type eventType, int eventID) {
            Event e = new Event(eventType, eventID);
            FireNormalEventType(e);
            FireNormalEventID(e);
        }


        /// <summary>
        /// 发送 sticky 事件
        /// </summary>
        public void SendStickEvent (Type eventType, int eventID) {
            bool consumed = false;
            Event e = new Event(eventType, eventID);
            consumed |= FireNormalEventType(e);
            consumed |= FireNormalEventID(e);

            if (!consumed && !mStickySyncEvents.Contains(e))
                mStickySyncEvents.Add(e);
        }

        #endregion



        #region Internal Fire Event

        /**
         * return True if there is consumer
         */
        private bool FireNormalEventType(Event e) {
            if (mTypeEventHandlers.ContainsKey(e.eventType)) {
                mTypeEventHandlers[e.eventType](e.eventType, e.eventID);
                return true;
            }
            return false;
        }

        /**
         * return True if there is consumer
         */
        private bool FireNormalEventID (Event e) {
            if (mIDEventHandlers.ContainsKey(e)) {
                mIDEventHandlers[e](e.eventType, e.eventID);
                return true;
            }
            return false;
        }

        /**
         * Fire Stick Event And remove this event from sticky event list
         */
        private void FireStickEvent (Event stickEvent, EventHandlerFunc callback) {
            mStickySyncEvents.Remove(stickEvent);
            callback(stickEvent.eventType, stickEvent.eventID);
        }

        #endregion

    }
}
