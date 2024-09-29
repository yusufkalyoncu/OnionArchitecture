using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Shared;

namespace OnionArchitecture.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }
        else
        {
            return new ObjectResult(result.Error)
            {
                StatusCode = result.Error?.StatusCode
            };
        }
    }

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Data);
        }
        else
        {
            return new ObjectResult(result.Error)
            {
                StatusCode = result.Error?.StatusCode
            };
        }
    }
}