﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script
{
    public class EventBusConfiguration : IEventBusConfiguration
    {
        /// <summary>
        /// Determines whether or not Subscriber exceptions are thrown
        /// </summary>
        /// <remarks>
        /// This is false by default, which will cause EventBus to catch & swallow any unhandled exceptions from subscribers
        /// When this is true, any exceptions thrown by a subscriber will be thrown - this will cause any subsequent subscribers to not receive the event
        /// </remarks>
        public bool ThrowSubscriberException { get; set; } = false;

        internal static EventBusConfiguration Default = new EventBusConfiguration();
    }
}
