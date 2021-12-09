using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhysicsWars.Application.Features.Technical.HealthChecks;

namespace PhysicsWars.WebApi.Controllers;

[ApiVersion("1.0")]
public class HealthController : ApiControllerBase
{
    [HttpGet]
    public async Task<string> Health()
    {
        var apiStatus = await Mediator.Send(new HealthStatusCommand());
        var databaseStatus = await Mediator.Send(new DatabaseHealthCommand());

        return $"{apiStatus}\n{databaseStatus}";
    }
}