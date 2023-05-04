using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;

namespace One.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private static readonly HttpClient client = new HttpClient();
        private const string ApiKey = "cc75a8c8c2cd4bca9bc18705ba46069b";
        private const string BaseUrl = "https://cloudandinnovation-openai.openai.azure.com/openai/deployments/openai-rnd/chat/completions?api-version=2023-03-15-preview";

        [HttpPost("send-message-to-gpt")]
        public async Task<IActionResult> UseChatGPT(string query)
        {
            client.DefaultRequestHeaders.Add("api-key", ApiKey);

            var message = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = query
                    }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        [HttpGet]
        [Route("DetectObject")]
        public async Task<IActionResult> DetectObject(string query)
        {
            return Ok("Test!");
        }
    }
}