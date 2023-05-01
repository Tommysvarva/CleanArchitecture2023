using Application.Examples.Commands.CreateExample;
using Application.Examples.Queries.GetExamples;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExamplesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ExampleDto>>> Get()
    {
        return await Mediator.Send(new GetExamplesQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateExampleCommand command)
    {
        return await Mediator.Send(command);
    }
}