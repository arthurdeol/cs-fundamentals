using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teste
{
    public class TESTE2
    {
        public class Schedule
        {
            public Schedule(int firstDay, int lastDay, int id)
            {
                Id = id;
                LastDay = lastDay;
                FirstDay = firstDay;
            }
            public int Id { get; set; }
            public int FirstDay { get; set; }
            public int LastDay { get; set; }
        }



        /*
         * Complete the 'countMeetings' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts following parameters:
         *  1. INTEGER_ARRAY firstDay
         *  2. INTEGER_ARRAY lastDay
         */

        public static int CountMeetings(List<int> firstDay, List<int> lastDay)
        {
            var schedules = new List<Schedule>();
            var count = firstDay.Count();
            CustomStopwatch sw = new CustomStopwatch();
            sw.Start();
            for (int i = 0; i < count ; i++)
            {
                schedules.Add(new Schedule(firstDay[i], lastDay[i], i + 1));
            }
            sw.Stop();
            schedules = schedules.OrderBy(x => x.LastDay).ToList();
            int meetings = 0;
            CustomStopwatch sw1 = new CustomStopwatch();
            sw1.Start();
            for (int i = 1; i <= count; i++)
            {
                var match = schedules.FirstOrDefault(x => x.LastDay >= i);

                if (match == null) continue;
                meetings++;
                schedules.Remove(match);
            }
            sw1.Stop();

            Console.WriteLine($"for Time elapsed: {sw.ElapsedMilliseconds} Milliseconds, StartAt: {sw.StartAt.Value}, EndAt: {sw.EndAt.Value}");
            Console.WriteLine($"for2 Time elapsed: {sw1.ElapsedMilliseconds} Milliseconds, StartAt: {sw1.StartAt.Value}, EndAt: {sw1.EndAt.Value}");

            return meetings;
        }



    }
}
