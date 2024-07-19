using Payment.API.Context;
using Payment.API.Repositories.Interface;

namespace Payment.API.Repositories.Concrete;

public class PaymentRepository(PaymentDbContext dbContext) : GenericRepository<Entities.Payment>(dbContext), IPaymentRepository;