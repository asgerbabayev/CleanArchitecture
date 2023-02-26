using CleanArchitecture.Application.Countries.Commands.CreateCountry;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Contries.Commands;

public class CreateCountryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateCountry()
    {
        var countryCommand = new CreateCountryCommand
        {
            Name = "Test Country",
            PhoneAreaCode = "Test Country Area Code"
        };


        int id = await SendAsycn(countryCommand);

        var country = await FindAsync<Country>(id);
        country.Should().NotBeNull();
        country!.Name.Should().Be(countryCommand.Name);
        country.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
