using Calculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Calculator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private readonly ILogger<CalculateController> _logger;
        private readonly IBatmanCalculatorService _batmanCalculatorService;
        private readonly IMemoryCache _memoryCache;

        public CalculateController(ILogger<CalculateController> logger,
            IBatmanCalculatorService batmanCalculatorService,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _batmanCalculatorService = batmanCalculatorService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get(int n, int k)
        {
            _logger.LogInformation($"Calculating result for n = {n} and k = {k}");

            _memoryCache.TryGetValue(new { n, k }, out var result);

            if (result is null)
            {
                result = _batmanCalculatorService.Calculate(n, k);

                _memoryCache.Set(new { n, k }, result);
            }

            _logger.LogInformation($"Result is {result}");

            return Ok(result);
        }
    }
}
