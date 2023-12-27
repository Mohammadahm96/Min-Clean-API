using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly CleanApiMainContext _dbContext;

    public HealthController(CleanApiMainContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult CheckHealth()
    {
        try
        {
            var result = _dbContext.Users.Any();

            return Ok("Database connection is good.");
        }
        catch (Exception ex)
        {
            // Log exception
            Console.WriteLine($"Database connection check failed: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
