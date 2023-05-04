using FluentValidation;

namespace Application.Examples.Commands.CreateExample;

public class CreateExampleCommandValidator: AbstractValidator<CreateExampleCommand>
{
    // Note: Ensure consistency with database configurations
    public CreateExampleCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(100)
            .NotEmpty();
    }
}