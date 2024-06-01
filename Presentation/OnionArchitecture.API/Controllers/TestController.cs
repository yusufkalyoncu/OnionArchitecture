using Microsoft.AspNetCore.Mvc;

namespace OnionArchitecture.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    private readonly IConfiguration _configuration;
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var res = _configuration["Test"];
        return View();
    }
}