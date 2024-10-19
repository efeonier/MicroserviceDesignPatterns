using EventSourcing.API.Context;
using EventSourcing.API.DTOs;
using EventSourcing.API.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.API.Handlers
{
    public class GetProductAllListByUserIdHandler(AppDbContext context) : IRequestHandler<GetProductAllListByUserId, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetProductAllListByUserId request, CancellationToken cancellationToken)
        {
            var products = await context.Products.Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken: cancellationToken);

            return products.Select(x => new ProductDto { Id = x.Id, Name = x.Name, Price = x.Price, Stock = x.Stock, UserId = x.UserId }).ToList();
        }
    }
}