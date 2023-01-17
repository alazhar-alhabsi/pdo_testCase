using System;
using System.Collections.Generic;

namespace pdo_testCase.models
{
    public class Result
    {
        public Result(int maxValue, int minValue, List<Events> events, List<string> eventTypeList)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            Events = events;
            EventTypeList = eventTypeList;
        }

       public int MaxValue { get; set; }
       public int MinValue { get; set; }
       public List<Events> Events { get; set; }
       public List<string> EventTypeList { get; set; }

    }
}
