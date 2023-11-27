using FluentValidation;

namespace Application.Examples.Commands.CreateExample;

public class CreateExampleCommandValidator: AbstractValidator<CreateExampleCommand>
{
    // Note: Remember to ensure consistency with database configurations
    public CreateExampleCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(100)
            .MinimumLength(2)
            .NotEmpty();
    }
}