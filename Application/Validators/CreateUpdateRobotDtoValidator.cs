namespace Application.Validators;

public class CreateUpdateRobotDtoValidator : AbstractValidator<CreateUpdateRobotDto> 
{
    public CreateUpdateRobotDtoValidator()
    {
        RuleFor(r => r.CodeName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(8);
    }
}
