using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Errors.Entity;
using OnionArchitecture.Domain.ValueObjects;

namespace Domain.UnitTests.Entities;

public class UserTests
{
    private readonly Phone ValidPhone;
    private readonly Email ValidEmail;
    private readonly Name ValidName;
    private readonly string ValidPassword;

    public UserTests()
    {
        ValidPhone = Phone.Create("90", "5555555555").Data!;
        ValidEmail = Email.Create("test@test.com").Data!;
        ValidName = Name.Create("Test", "Test").Data!;
        ValidPassword = "1234567";
    }
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CreateUserEntity_Should_ReturnError_WhenPasswordIsNullOrEmpty(string? password)
    {
        //Arrange
        
        //Act
        var userResult = User.Create(ValidName, ValidEmail, ValidPhone, password, password);
        //Assert
        Assert.NotNull(userResult?.Error);
        Assert.Equal(UserErrors.PasswordNullOrEmpty, userResult?.Error);
    }
    
    [Theory]
    [InlineData("12345")]
    [InlineData("1234567890123456789012345678901")]
    public void CreateUserEntity_Should_ReturnError_WhenPasswordLengthInvalid(string password)
    {
        //Arrange
        
        //Act
        var userResult = User.Create(ValidName, ValidEmail, ValidPhone, password, password);
        //Assert
        Assert.NotNull(userResult?.Error);
        Assert.Equal(UserErrors.PasswordInvalidLength, userResult?.Error);
    }
    
    [Fact]
    public void CreateUserEntity_Should_Succeed_WithValidInputs()
    {
        //Arrange
        
        //Act
        var userResult = User.Create(ValidName, ValidEmail, ValidPhone, ValidPassword, ValidPassword);
        //Assert
        Assert.NotNull(userResult);
        Assert.Null(userResult.Error);
        Assert.NotNull(userResult.Data);
    }
}