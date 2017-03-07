using RestAPISample.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestAPISample.Client.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:54741/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var users = GetUsers(client);

            foreach (var item in users.Result)
            {
                Console.WriteLine(item.Id + " " + item.Name);
            }

            Console.ReadLine();
        }

        static async Task<List<User>> GetUsers(HttpClient client)
        {
            var users = new List<User>();

            using (client)
            {
                HttpResponseMessage response = await client.GetAsync("api/user");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<User>>();
                }
            }

            return users;
        }
    }
}
