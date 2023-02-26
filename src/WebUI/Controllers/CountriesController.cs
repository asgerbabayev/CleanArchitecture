using CleanArchitecture.Application.Countries.Commands.CreateCountry;
using CleanArchitecture.Application.Countries.Commands.DeleteCountry;
using CleanArchitecture.Application.Countries.Commands.UpdateCountry;
using CleanArchitecture.Application.Countries.Queries;
using System.Net;

namespace CleanArchitecture.WebUI.Controllers;

[Route("api/[controller]")]
public class CountriesController : ApiControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var countries = await Mediator.Send(new GetCountriesQuery());
        return Ok(value: countries);
    }
     
    [HttpPost]
    public async Task<IActionResult> Post(CreateCountryCommand command)
    {
        await Mediator.Send(command);
        return Ok(value: command);
    }
     
    [HttpPut]
    public async Task<IActionResult> Put(UpdateCountryCommand command)
    {
        await Mediator.Send(command);
        return Ok(value: command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    { 
        await Mediator.Send(new DeleteCountryCommand(id));
        return Ok(value: HttpStatusCode.OK);
    }
}
