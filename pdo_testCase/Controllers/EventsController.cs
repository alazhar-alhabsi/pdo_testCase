using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pdo_testCase.models;

namespace pdo_testCase.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        

        [HttpPost]
        public IActionResult processEvents(List<Events> EventList)
        {

            var finalArr = new List<Events>();
            var events = EventList.Where(e => e.name != null).Select(e => e.name);
            var eventTypeList = events.Distinct().ToList();
            var min = EventList.Min(e => e.start);
            var max = EventList.Max(e => e.end);
            int minWidth = 40;
            int index = eventTypeList.Capacity;


            foreach (var e in eventTypeList)
            {
                var random = new Random();
                var haxColor = String.Format("#{0:X6}", random.Next(0x1000000));
             

                var final = EventList.Where(f => f.name == e).ToList();
                var d = MergeOverlappingEvents(final, e);

                d.ForEach((e) =>
                {
                    e.width = index * minWidth;
                    e.color = haxColor;
                });

                finalArr.AddRange(d);
                index--;
            }


            Result resutl = new Result (max, min, finalArr, eventTypeList);
            return Ok(resutl);
        }


        public List<Events> MergeOverlappingEvents(List<Events> data, string eventType)
        {
           

            data.Sort((a, b) => a.start.CompareTo(b.end));

            for (int i = 0; i < data.Count - 1; i++)
            {

                if (data[i].end >= data[i + 1].start)
                {
                    data[i].end = Math.Max(data[i].end, data[i + 1].end);
                    data[i].name = eventType;
                    data.RemoveAt(i + 1);
                    i--;
                }
            }

            return data;
        }
    }






}
