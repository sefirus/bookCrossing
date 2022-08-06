using Core.ViewModels.CategoryViewModels;
using FluentValidation;

namespace WebApi.Validators.CategoryValidators;

public class CategoryValidatorBase<T> : AbstractValidator<T> where T : CategoryVmBase
{
    public CategoryValidatorBase()
    {
        RuleFor(cat => cat.Description)
            .MinimumLength(1)
            .WithMessage("Minimum description length must be greater than 1")
            .MaximumLength(600)
            .WithMessage("Maximum description length must be lower than 600");        
        
        RuleFor(cat => cat.Name)
            .MinimumLength(1)
            .WithMessage("Minimum name length must be greater than 1")
            .MaximumLength(150)
            .WithMessage("Maximum name length must be lower than 150");
    }
}