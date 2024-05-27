using Microsoft.AspNetCore.Mvc;
using Rahtk.Shared.Models;

namespace Rahtk.Api.Utils
{
    public static class ResponseResult
    {

        public static IActionResult ToResult<T>(this BaseResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.statusCode
            };
        }
    }
}

