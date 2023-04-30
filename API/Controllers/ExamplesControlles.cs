using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PresentationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Example>>> Get()
    {
        return new List<Example>
        {
            new Example
            {
                Id = 1,
                Title = "First example"
            },
            new Example
            {
                Id = 2,
                Title = "Second example"
            },
        };
    }

    [HttpPost]
    public async Task<ActionResult> Create(string title)
    {
        await Task.Run(() =>
        {
            var presentation = new Example
            {
                Title = title
            };
        });

        return Ok();
    }
}