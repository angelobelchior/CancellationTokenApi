using Microsoft.AspNetCore.Mvc;

namespace CancellationTokenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesReportController : ControllerBase
{
    private readonly ILogger<SalesReportController> _logger;
    public SalesReportController(ILogger<SalesReportController> logger)
        => _logger = logger;

    [HttpGet]
    public async Task<IEnumerable<Sale>> Get(
        DateOnly startDate, 
        DateOnly endDate, 
        CancellationToken cancellationToken)
    {
        try
        {
            // Simula uma operação lenta
            _logger.LogInformation("Simula uma operação lenta");
            await Task.Delay(5000, cancellationToken);
            
            return new List<Sale>
            {
                new (1, new DateOnly(2023, 01, 01), 100),
                new (2, new DateOnly(2023, 01, 02), 200),
                new (3, new DateOnly(2023, 01, 03), 300),
                new (4, new DateOnly(2023, 01, 04), 400),
                new (5, new DateOnly(2023, 01, 05), 500),
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Esse erro ocorreu pelo fato de o usuário ter cancelado a requisição");
            return Enumerable.Empty<Sale>();
        }
    }
}

public record Sale(int Id, DateOnly Date, decimal Value);