using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace M7CaAPIE
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int oldestAge = 0;
            string oldestName = "";
            int counting = 1;
            do
            {
                Console.WriteLine("Enter a name: ");
                string name = Console.ReadLine()!;
                try
                {
                    using HttpClient client = new();

                    string url = $"https://api.agify.io?name={name}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(jsonResponse);
                    string age = json["age"]?.ToString()!;
                    string count = json["count"]?.ToString()!;

                    Console.WriteLine($"Name {counting}: {name} is apx {age} years old");

                    if (Convert.ToInt32(age) > oldestAge)
                    {
                        oldestAge = Convert.ToInt32(age);
                        oldestName = name!;
                    }
                    counting++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed! {ex}");
                }
            }
            while (counting < 4);
            Console.WriteLine($"{oldestName} is the oldest!");
        }
    }
}