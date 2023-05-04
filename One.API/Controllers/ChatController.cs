using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using One.Core.Model;
using System.Text.Json;
using System.Text;

namespace One.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        [Route("UseChatGPT")]
        public async Task<IActionResult> UseChatGPT(string query)
        {
            string OutPutResult = "";

            //CompletionRequest completionRequest = new CompletionRequest
            //{
            //    Model = "text-davinci-003",
            //    Prompt = query,
            //    MaxTokens = 1000
            //};

            CompletionRequest completionRequest = new CompletionRequest
            {
                Model = "text-davinci-003",
                Prompt = query,
                MaxTokens = 1000
            };

            CompletionResponse completionResponse = new CompletionResponse();

          

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://cloudandinnovation-openai.openai.azure.com/openai/deployments/openai-rnd/chat/completions?api-version=2023-03-15-preview");
            request.Headers.Add("api-key", "<API_KEY>");


            //string requestString = JsonSerializer.Serialize(completionRequest);
            //request.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            //var content = new StringContent({ "messages": [{ "role": "user", "content": query}] }  , Encoding.UTF8, "application/json"); 
            //var content = new StringContent("{\r\n \"messages\": [{\"role\": \"user\", \"content\": \"Adam Simith'i tanıyo musun?\"}]\r\n }", null, "application/json");


            ChatMessage chatMessage = new ChatMessage
            {
                Role = "user",
                Content = query
            };

            string requestString = JsonSerializer.Serialize(chatMessage);

            var content = new StringContent(requestString, Encoding.UTF8, "application/json");

            request.Content = content; 
            var response = await client.SendAsync(request); 
            response.EnsureSuccessStatusCode(); 
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return Ok(response.Content.ReadAsStringAsync());
        }

        [HttpGet]
        [Route("DetectObject")]

        public async Task<IActionResult> DetectObject(string query)
        {
            return Ok("Test!");
        }
    }
}
