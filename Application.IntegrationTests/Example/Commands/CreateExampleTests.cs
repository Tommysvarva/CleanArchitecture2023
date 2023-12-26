using Application.Examples.Commands.CreateExample;

namespace Application.IntegrationTests.Example.Commands;

public class CreateExampleTests : BaseIntegrationTest
{
    public CreateExampleTests(CustomWebApplicationFactory apiFactory)
    :base(apiFactory) { }

    [Fact]
    public async Task Create_ShouldCreate_Example()
    {
        var command = new CreateExampleCommand
        { 
            Title = "example"
        };

        await Sender.Send(command);

        var example = DbContext.Examples.FirstOrDefault(e => e.Title == "example");
        
        Assert.NotNull(example);
    }
}