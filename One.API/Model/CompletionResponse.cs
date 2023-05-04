using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace One.Core.Model
{
    public class CompletionResponse
    {
        [JsonPropertyName("choices")]
        public List<ChatGPTChoice>? Choices { get; set; }

        [JsonPropertyName("usage")]
        public ChatGPTUsage? Usage { get; set; }
    }
}
