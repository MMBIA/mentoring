using CatalogService.Application.Common.Exceptions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Items.Commands.DeleteItem;

public record DeleteItemCommand(int Id) : IRequest;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        _context.Items.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
