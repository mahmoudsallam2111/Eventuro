using FluentValidation;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
internal sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.categoryName).NotEmpty().WithMessage("category name should not be empty");
    }
}
