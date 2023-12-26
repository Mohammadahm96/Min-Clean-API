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
            // Attempt to query a small piece of data from the database
            var result = _dbContext.Users.Any();

            return Ok("Database connection is healthy.");
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Database health check failed: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
