using Application.Common.Mappers;
using Application.Examples.Queries.GetExamples;
using Domain.Entities;
using Xunit;

namespace Application.UnitTests.Common.Mappers;

public class ExampleMapperTests
{
    [Fact]
    public void CreateDto_ReturnsSingleExampleDto_WhenGivenSingleExample()
    {
        //Arrange
        var exampleMapper = new ExampleMapper();
        var example = new Example
        {
            Title = "example",
        };
        
        //Act
        var exampleDto = exampleMapper.CreateDto(example);
        
        //Assert
        Assert.Equal(example.Title, exampleDto.Title);
    }

    [Fact]
    public void CreateDto_ReturnsListOfExampleDto_WhenGivenListOfExamples()
    {
        //Arrange
        var exampleMapper = new ExampleMapper();
        var exampleList = new List<Example>
        {
            new Example
            {
                Title = "example1"
            },
            new Example
            {
                Title = "example2"
            }
        };
        
        //Act
        var exampleDtoList = exampleMapper.CreateDto(exampleList);
        
        //Assert
        Assert.NotEmpty(exampleDtoList);
        Assert.Equal(exampleList.Count, exampleDtoList.Count);
        Assert.Equal("example1", exampleDtoList[0].Title);
        Assert.Equal("example2", exampleDtoList[1].Title);
    }
    
}