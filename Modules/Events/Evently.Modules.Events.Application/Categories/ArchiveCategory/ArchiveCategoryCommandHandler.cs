using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Category;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;
internal sealed class ArchiveCategoryCommandHandler : ICommandHandler<ArchiveCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IUnitOfWork unitOfWork;

    public ArchiveCategoryCommandHandler(ICategoryRepository categoryRepository , IUnitOfWork unitOfWork)
    {
        this.categoryRepository = categoryRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetAsync(request.categoryId, cancellationToken);    

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.categoryId));
        }

        if (category.IsArchived)
        {
            return Result.Failure(CategoryErrors.AlreadyArchived);
        }
        category.Archive();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
