using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
