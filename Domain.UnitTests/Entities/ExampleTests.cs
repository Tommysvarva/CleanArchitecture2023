namespace Domain.UnitTests.Entities;

public class ExampleTests
{
    [Fact]

    public void ExampleEntity_Title_ShouldNotBeNull()
    {
        // Arrange
        var entity = new Example { Title = "test" };
        
        // Assert
        Assert.NotEmpty(entity.Title);
    }
}