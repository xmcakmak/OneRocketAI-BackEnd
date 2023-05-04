using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace One.Core.Model
{
    public class ChatMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
}
