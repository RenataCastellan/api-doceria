using Microsoft.AspNetCore.Mvc;

namespace api_doceria.Controllers;

[Route("/")]
[ApiController]
public class PrincipalController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new { api = "ApiDoceria", status = "up" });
    }
}