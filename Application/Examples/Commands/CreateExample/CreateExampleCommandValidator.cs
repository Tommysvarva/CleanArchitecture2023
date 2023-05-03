using FluentValidation;

namespace Application.Examples.Commands.CreateExample;

public class CreateExampleCommandValidator: AbstractValidator<CreateExampleCommand>
{
    public CreateExampleCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}