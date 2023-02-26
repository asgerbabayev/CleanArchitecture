namespace CleanArchitecture.Application.Countries.Validations;
public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(c => c.Id)  
            .NotEmpty()
            .WithMessage("Id required");

        RuleFor(c => c.Name)
            .MaximumLength(200)
            .WithMessage("maximum length must be 200")
            .NotEmpty()
            .WithMessage("CountryName required");
    }
}