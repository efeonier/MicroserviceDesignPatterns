using Order.API.Context;
using Order.API.Repositories.Interface;

namespace Order.API.Repositories.Concrete;

public class OrderRepository(OrderDbContext dbContext) : GenericRepository<Entities.Order>(dbContext), IOrderRepository;