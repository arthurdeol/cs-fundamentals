using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using Newtonsoft.Json;

public class Location
{
        public int id { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int zipCode { get; set; }
}

public class DataItem
{
        public int id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string timestamp { get; set;}
        public string txnType { get; set; }
        public string amount { get; set; }
        public Location location { get; set; }
        public string ip { get; set; }
}

public class Post
{
        public string page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List <DataItem> data { get; set; }
}

public class User
{
    public int idUser { get; set; }
    public double amountTotal { get; set; }
    public List<double> amouts { get; set; }
}
class Result
{

    /*
     * Complete the 'totalTransactions' function below.
     *
     * The function is expected to return a 2D_INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER locationId
     *  2. STRING transactionType
     */

    public static List<List<int>> totalTransactions(int locationId, string transactionType)
    {
        var client = new WebClient();
        var text = client.DownloadString($"https://jsonmock.hackerrank.com/api/transactions/search?txnType={transactionType}&page=1");
        Post post = JsonConvert.DeserializeObject<Post>(text);
        var pages = post.total_pages;
        var datas = post.data.Where(x => x.location.id == locationId).ToList();
        for (int i = 2; i <= pages; i++)
        {
            var jsonObjt = client.DownloadString($"https://jsonmock.hackerrank.com/api/transactions/search?txnType={transactionType}&page={i}");
            Post postObjt = JsonConvert.DeserializeObject<Post>(jsonObjt);
            postObjt.data.Where(x => x.location.id == locationId).ToList().ForEach(x => {
                datas.Add(x);
            });
        }

        List<List<int>> result = new List<List<int>>();
        List<User> users = new List<User>();
        datas.ForEach(d => {
            List<int> list = new List<int>();
            string amount = d.amount.Trim('$');
            amount = amount.Replace(",", "").Replace(".",",");
            double valor = Double.Parse(amount);
            int valueInt = (int)Math.Round(valor);
            List<int> listIds = users.Select(x => x.idUser).ToList();
            if (!listIds.Contains(d.userId))
            {
                var user = new User();
                user.idUser = d.userId;               
                user.amouts = new List<double>();
                user.amouts.Add(valor);
                users.Add(user);
            }
            else
            {
                var user = users.FirstOrDefault(x => x.idUser == d.userId);
                user.amouts.Add(valor);
            }
            list.Add(d.userId);
            list.Add(valueInt);
            result.Add(list);
        });
        var result2 = new List<List<int>>();
        users.ForEach(user =>
        {
            user.amountTotal = user.amouts.Sum();
            string total = user.amountTotal.ToString().Remove(user.amountTotal.ToString().Length - 2, 2);
            List<int> userTotal = new List<int> {user.idUser, (int)user.amountTotal };
            result2.Add(userTotal);
        });
        return result2.OrderBy(x => x[0]).ToList();

    }

}

// class Solution
// {
//     public static void Main(string[] args)
//     {
//         TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

//         int locationId = Convert.ToInt32(Console.ReadLine().Trim());

//         string transactionType = Console.ReadLine();

//         List<List<int>> result = Result.totalTransactions(locationId, transactionType);

//         textWriter.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));

//         textWriter.Flush();
//         textWriter.Close();
//     }
// }
