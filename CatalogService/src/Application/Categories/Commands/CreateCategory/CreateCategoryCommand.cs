using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Categories.Commands.CreateCategory;
public record CreateCategoryCommand : IRequest<int>
{
    public string Name { get; init; }
    public string Image { get; init; }
    public int? ParentCategoryId { get; init; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            Name = request.Name, Image = request.Image, ParentCategoryId = request.ParentCategoryId
        };

        _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
