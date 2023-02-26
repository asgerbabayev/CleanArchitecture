namespace CleanArchitecture.Application.Countries.Validations;
public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(200)
            .WithMessage("maximum length must be 200")
            .NotEmpty()
            .WithMessage("CountryName required");
    }
}
