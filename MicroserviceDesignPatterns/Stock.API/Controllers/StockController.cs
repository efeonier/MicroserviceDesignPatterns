using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.API.Repositories.Interface;

namespace Stock.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;
    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var stockList = await _stockRepository.Get().ToListAsync();
        if (stockList.Count > 0)
        {
            return Ok(stockList);
        }
        return NotFound(stockList);
    }
}