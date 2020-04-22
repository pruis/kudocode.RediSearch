using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KudoCode.LogicLayer.Dtos
{
    public static class Constants
    {
        public static class EventQueues
        {
            public const string AggregateEventsQueue = "AggregateEventsQueue";
            public const string InternalEventsQueue = "InternalEventsQueue";

            public static List<string> Queue = new List<string>()
            {
                InternalEventsQueue,
                AggregateEventsQueue
            };
        }
    }
}