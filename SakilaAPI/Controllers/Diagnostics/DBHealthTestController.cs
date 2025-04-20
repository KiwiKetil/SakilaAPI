using Microsoft.AspNetCore.Mvc;
using SakilaAPI.Data;

namespace SakilaAPI.Controllers.Diagnostics;

[ApiController]
[Route("api/dbhealth")]
public class DBHealthController : ControllerBase
{
    private readonly SakilaContext _dbContext;

    public DBHealthController(SakilaContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet(Name = "DBHealth")]     
    public async Task<IActionResult> TestDatabaseConnection()
    {                  
        var canConnect = await _dbContext.Database.CanConnectAsync();
        if (!canConnect)
            return StatusCode(500, "EF Core could not establish a database connection.");

        return Ok("Database connection successful!");
    
    }
}
