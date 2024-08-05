using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void CreateEmailValueObject_Should_ReturnError_WhenEmailIsNullOrEmpty()
    {
        //Arrange
        
        //Act
        var result = Email.Create(null);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(EmailErrors.EmailNullOrEmpty, result.Error);
    }
    
    [Theory]
    [InlineData("test.com")]
    [InlineData("test@testcom")]
    public void CreateEmailValueObject_Should_ReturnError_WhenEmailIsInvalid(string email)
    {
        //Arrange
        
        //Act
        var result = Email.Create(email);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(EmailErrors.InvalidFormat, result.Error);
    }
    
    [Theory]
    [InlineData("test@test.com")]
    [InlineData("t@t.c")]
    public void CreateEmailValueObject_Should_Succeed_WithValidInputs(string email)
    {
        //Arrange
        
        //Act
        var result = Email.Create(email);
        
        //Assert
        Assert.NotNull(result);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        Assert.Equal(email, result.Data.Value);
    }
}