using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace One.Service.Services
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public ImageService(string model, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _model = model;
            //_apiKey = apiKey;
        }

        public async Task<string> RecognizeObjectsInImage(string imageUrl)
        {
            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
            var base64Image = Convert.ToBase64String(imageBytes);

            var prompt = $"Please describe the objects in this image: {imageUrl}\n\nObjects:";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://api.openai.com/v1/engines/{_model}/completions"),
                Headers =
                {
                    { "Authorization", $"Bearer <API_KEY>" }
                },
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    prompt,
                    max_tokens = 1024,
                    n = 1,
                    stop = "",
                    temperature = 0.5,
                    model = _model,
                    prompt_suffix = ""
                }))
                {
                    Headers =
                    {
                        ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
                    }
                }
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();


            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<OpenAIResponse>(responseString);

            
            return jsonResponse.Completions[0].Text.Trim();

        }


    }

    public class OpenAIResponse
    {
        [JsonPropertyName("completions")]
        public List<Completion> Completions { get; set; }
    }

    public class Completion
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
