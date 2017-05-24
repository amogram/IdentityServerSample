using System;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace TheLibary.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DoTheThing();

            Console.Read();
        }

        private static async void DoTheThing()
        {
            Console.WriteLine("--------- Start Token Response ----------");
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("--------- End Token Response ----------");

            Console.WriteLine("--------- Call the API ----------");
            // call api
            Console.WriteLine("1. Attempt to call the API without the bearer token ----------");

            var client = new HttpClient();
            var noAuthresponse = await client.GetAsync("http://localhost:5001/api/values");
            if (!noAuthresponse.IsSuccessStatusCode)
            {
                Console.WriteLine(noAuthresponse.StatusCode);
                Console.WriteLine("See? You need to be authorized. Clever, eh?");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.ReadLine();


            Console.WriteLine("2. Attempt to call the API with the bearer token ----------");

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
                Console.WriteLine("Oh my! It worked!");
            }
            Console.WriteLine("--------- End Call the API ----------");

            Console.ReadLine();

        }
    }
}