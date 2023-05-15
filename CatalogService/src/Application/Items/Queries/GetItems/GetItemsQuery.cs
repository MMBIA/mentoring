using AutoMapper;
using AutoMapper.QueryableExtensions;
using CatalogService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Items.Queries.GetItems;

public record GetItemsQuery : IRequest<List<ItemDto>>
{
    public int CategoryId { get; set; }
}

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<ItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Items
            .Where(x => x.CategoryId == request.CategoryId)
            .OrderBy(x => x.Name)
            .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
