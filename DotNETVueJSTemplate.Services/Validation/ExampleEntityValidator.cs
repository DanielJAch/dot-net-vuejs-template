using DotNETVueJSTemplate.Services.Dtos;
using FluentValidation;

namespace DotNETVueJSTemplate.Services.Validation
{
    public class ExampleEntityValidator : AbstractValidator<ExampleEntityDto>
    {
        public ExampleEntityValidator()
        {
            this.RuleFor(m => m.ExampleProperty)
                .NotEmpty()
                .WithMessage("ExampleProperty is required.")
                .MaximumLength(500)
                .WithMessage("ExampleProperty cannot exceed 500 characters.");
        }
    }
}
