using System;
using System.Collections.Generic;

namespace teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<int> firstDay = new List<int> {5,1,2,1,2,2};
            //List<int> lastDay = new List<int> {5,3,2,1,3,3};
            //int result = TESTE2.CountMeetings(firstDay, lastDay);
            //Console.WriteLine(result);
            //var result = Result.totalTransactions(1, "debit");
            //result.ForEach(r =>
            //{
            //    r.ForEach(z => Console.WriteLine(z.ToString()));
            //});

            List<int> requirements = new List<int> { 4,6};
            int flaskTypes = 2;
            List<List<int>> markings = new List<List<int>>();
            List<int> mark1 = new List<int> { 0, 5 };
            markings.Add(mark1);
            List<int> mark2 = new List<int> { 0, 7 };
            markings.Add(mark2);
            List<int> mark3 = new List<int> { 0, 10 };
            markings.Add(mark3);
            List<int> mark4 = new List<int> { 1, 4 };
            markings.Add(mark4);
            List<int> mark5 = new List<int> { 1, 10 };
            markings.Add(mark5);
            int result = teste3.ChooseFlask(requirements, flaskTypes, markings);
            Console.WriteLine(result);
        }
    }
}
