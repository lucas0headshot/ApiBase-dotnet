using ApiBase;

[Route("api/[controller]")]
public class HealthCheckController : BaseApiController
{
    [HttpGet]
    public IActionResult Get() => Ok("Funcionando!");
}