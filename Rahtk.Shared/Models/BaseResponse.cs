using System.Text.Json;

namespace Rahtk.Shared.Models
{
    public class BaseResponse<T>
    {
        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

