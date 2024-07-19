using Stock.API.Context;
using Stock.API.Repositories.Interface;

namespace Stock.API.Repositories.Concrete;

public class StockRepository(StockDbContext dbContext) : GenericRepository<Entities.Stock>(dbContext), IStockRepository;