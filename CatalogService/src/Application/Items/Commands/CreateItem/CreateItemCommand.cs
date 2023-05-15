using System.ComponentModel;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Items.Commands.CreateItem;

public record CreateItemCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new Item
            {
                Name = request.Name, 
                Description = request.Description, 
                Image = request.Image, 
                Price = request.Price,
                Amount = request.Amount,
                CategoryId = request.CategoryId
            };

        _context.Items.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}