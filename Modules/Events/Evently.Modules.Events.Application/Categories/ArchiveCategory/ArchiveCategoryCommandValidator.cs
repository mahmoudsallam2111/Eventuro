using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;
internal sealed class ArchiveCategoryCommandValidator : AbstractValidator<ArchiveCategoryCommand>   
{
    public ArchiveCategoryCommandValidator()
    {
        RuleFor(c => c.categoryId).NotEmpty();  
    }
}
