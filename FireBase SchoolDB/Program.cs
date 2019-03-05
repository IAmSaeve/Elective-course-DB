using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace FireBase_SchoolDB
{
    class Program
    {
        const string URI = "https://schooldb-8ea56.firebaseio.com/";

        static void Main(string[] args)
        {
            Console.WriteLine(GetSubjects());
        }

        private static Dictionary<string, dynamic> GetSubjects()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(URI + "Subject/.json").Result;
                Dictionary<string, dynamic> data =
                    JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Content.ReadAsStringAsync().Result);

                return data;
            }
        }
    }
}
