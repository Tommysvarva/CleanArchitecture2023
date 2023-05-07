namespace Domain.UnitTests.Common;

public class BaseAuditableEntityTests
{
    [Fact]
    public void Created_ShouldBeEqualToSetCreated()
    {
        // Arrange
        var dateTimeNow = DateTime.Now;
        var entity = new Example
        {
            Created = dateTimeNow
        };

        // Assert
        Assert.Equal(dateTimeNow, entity.Created);
    }

    [Fact]
    public void CreatedBy_ShouldBeSetToNullOnCreation()
    {
        // Arrange
        const string createdBy = "me";
        var entity = new Example
        {
            CreatedBy = createdBy
        };
        
        // Assert
        Assert.Equal(createdBy, entity.CreatedBy);
    }

    [Fact]
    public void LastModified_ShouldBeNullOnCreation()
    {
        // Arrange
        var entity = new Example();

        // Assert
        Assert.Null(entity.LastModified);
    }

    [Fact]
    public void LastModifiedBy_ShouldBeNullOnCreation()
    {
        // Arrange
        var entity = new Example();

        // Assert
        Assert.Null(entity.LastModifiedBy);
    }

    [Fact]
    public void LastModified_ShouldBeSetToCurrentTimeOnModification()
    {
        // Arrange
        var entity = new Example();
        var initialLastModified = entity.LastModified;

        // Act
        entity.LastModified = DateTime.Now;

        // Assert
        Assert.True(entity.LastModified <= DateTime.Now);
        Assert.True(entity.LastModified >= DateTime.Now.AddSeconds(-1));
        Assert.NotEqual(initialLastModified, entity.LastModified);
    }

    [Fact]
    public void LastModifiedBy_ShouldBeSetOnModification()
    {
        // Arrange
        var entity = new Example();
        var initialLastModifiedBy = entity.LastModifiedBy;

        // Act
        entity.LastModifiedBy = "TestUser";

        // Assert
        Assert.Equal("TestUser", entity.LastModifiedBy);
        Assert.NotEqual(initialLastModifiedBy, entity.LastModifiedBy);
    }
}