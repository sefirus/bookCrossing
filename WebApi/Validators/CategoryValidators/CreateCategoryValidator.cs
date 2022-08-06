using Core.ViewModels.CategoryViewModels;
using FluentValidation;

namespace WebApi.Validators.CategoryValidators;

public class CreateCategoryValidator : CategoryValidatorBase<CreateCategoryViewModel>
{
    public CreateCategoryValidator() 
    {
        RuleFor(cat => cat.ParentCategoryId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Invalid parent category id");
    }
}