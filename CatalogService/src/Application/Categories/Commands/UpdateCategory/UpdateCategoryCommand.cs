using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public int? ParentCategoryId { get; init; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        entity.Name = request.Name;
        entity.Image = request.Image;
        entity.ParentCategoryId = request.ParentCategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
