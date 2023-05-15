using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Items.Commands.CreateItem;
using FluentValidation;

namespace CatalogService.Application.Items.Commands.CreateItem;
public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
    }
}
