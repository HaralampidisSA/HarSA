using FluentValidation;
using TestApi.V1.ViewModels;

namespace TestApi.V1.Validators
{
    public class AddValidator : AbstractValidator<AddVM>
    {
        public AddValidator()
        {
            RuleFor(x => x.Code).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
