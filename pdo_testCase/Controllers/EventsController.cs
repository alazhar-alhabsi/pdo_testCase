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
            var events = EventList.Where(e => e.Event != null).Select(e => e.Event);
            var eventTypeList = events.Distinct().ToList();
            var min = EventList.Min(e => e.Start);
            var max = EventList.Max(e => e.End);

            foreach (var e in eventTypeList)
            {
                var final = EventList.Where(f => f.Event == e).ToList();
                var d = MergeOverlappingEvents(final, e);
                finalArr.AddRange(d);
            }


            Result resutl = new Result (max, min, finalArr, eventTypeList);
            return Ok(resutl);
        }


        public List<Events> MergeOverlappingEvents(List<Events> data, string eventType)
        {
            data.Sort((a, b) => a.Start.CompareTo(b.Start));

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].End >= data[i + 1].Start)
                {
                    data[i].End = Math.Max(data[i].End, data[i + 1].End);
                    data[i].Event = eventType;
                    data.RemoveAt(i + 1);
                    i--;
                }
            }

            return data;
        }
    }






}
