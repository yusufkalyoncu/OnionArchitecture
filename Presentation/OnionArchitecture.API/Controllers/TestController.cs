using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnionArchitecture.Application.Options;

namespace OnionArchitecture.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly PostgreOptions _options;

    public TestController(IOptions<PostgreOptions> options)
    {
        _options = options.Value;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok(_options);
    }
}