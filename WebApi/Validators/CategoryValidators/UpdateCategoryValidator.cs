using Core.ViewModels.CategoryViewModels;
using FluentValidation;

namespace WebApi.Validators.CategoryValidators;

public class UpdateCategoryValidator : CategoryValidatorBase<UpdateCategoryViewModel>
{
    public UpdateCategoryValidator()
    {
        RuleFor(cat => cat.Id)
            .GreaterThan(1)
            .WithMessage("Invalid category id");
    }
}