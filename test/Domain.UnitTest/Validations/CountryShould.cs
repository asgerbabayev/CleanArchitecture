using CleanArchitecture.Application.Countries.Commands.CreateCountry;
using CleanArchitecture.Application.Countries.Validations;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanArchitecture.Domain.UnitTest.Validations;
public class CountryShould
{

    [Fact]
    public void CountryCreateValidation_NameNullOrEmpty_ReturnOk()
    { 
        // Arrange
        var validation = new CreateCountryCommandValidator();
        var testRequestModel = new CreateCountryCommand()
        {
            Name = null,
            PhoneAreaCode = null
        };

        // Act 
        var result = validation.TestValidate(testRequestModel);

        // Assert 
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void CountryCreateValidation_NameNullOrEmpty_ReturnError()
    {
        // Arrange
        var validation = new CreateCountryCommandValidator();
        var testRequestModel = new CreateCountryCommand()
        {
            Name = "Turkey",
            PhoneAreaCode = null
        };

        // Act 
        var result = validation.TestValidate(testRequestModel);

        // Assert 
        result.ShouldNotHaveValidationErrorFor(x => x.Name); 
    }
}
