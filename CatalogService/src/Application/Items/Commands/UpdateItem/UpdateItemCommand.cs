using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Items.Commands.UpdateItem;

public record UpdateItemCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.Amount = request.Amount;
        entity.CategoryId = request.CategoryId;
        entity.Image = request.Image;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}