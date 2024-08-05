using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class NameTests
{
    private const string FirstName = "TestName";
    private const string LastName = "TestLastName";
    
    [Fact]
    public void CreateNameValueObject_Should_ReturnError_WhenFirstNameIsNullOrEmpty()
    {
        //Arrange
        
        //Act
        var result = Name.Create(null, LastName);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(NameErrors.FirstNameNullOrEmpty, result.Error);
    }
    
    [Fact]
    public void CreateNameValueObject_Should_ReturnError_WhenLastNameIsNullOrEmpty()
    {
        //Arrange
        
        //Act
        var result = Name.Create(FirstName, null);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(NameErrors.LastNameNullOrEmpty, result.Error);
    }
    
    [Theory]
    [InlineData("t","test")]
    [InlineData("testtesttesttesttesttesttesttest","test")]
    public void CreateNameValueObject_Should_ReturnError_WhenFirstNameInvalidLength(string firstName, string lastName)
    {
        //Arrange
        
        //Act
        var result = Name.Create(firstName, lastName);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(NameErrors.FirstNameLengthError, result.Error);
    }
    
    [Theory]
    [InlineData("test","t")]
    [InlineData("test","testtesttesttesttesttesttesttest")]
    public void CreateNameValueObject_Should_ReturnError_WhenLastNameInvalidLength(string firstName, string lastName)
    {
        //Arrange
        
        //Act
        var result = Name.Create(firstName, lastName);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(NameErrors.LastNameLengthError, result.Error);
    }
}