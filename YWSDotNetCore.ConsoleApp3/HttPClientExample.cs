using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWSDotNetCore.ConsoleApp3
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _postEndPoint = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            _client = new HttpClient();
        }
        public async Task Read()
        {
            var response = await _client.GetAsync(_postEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            var response = await _client.GetAsync($"{_postEndPoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Create(string title,string body,int userId)
        {
            PostModel requestModel = new PostModel()
            {
                body = body,
                title = title,
                userId = userId
            };
           
            var jsonrequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonrequest,Encoding.UTF8,Application.Json);
            var response = await _client.PostAsync(_postEndPoint, content);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Update(int id,string title, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                id = id,
                body = body,
                title = title,
                userId = userId
            };

            var jsonrequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonrequest, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_postEndPoint}/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
    }

    public class PostModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

}
