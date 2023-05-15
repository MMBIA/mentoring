using FluentValidation;

namespace CatalogService.Application.Categories.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
    }
}
