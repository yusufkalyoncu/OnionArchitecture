using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class PhoneTests
{
    private const string CountryCode = "90";
    private const string PhoneNumber = "5555555555";
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenCountryCodeIsNull()
    {
        //Arrange

        //Act
        var result = Phone.Create(null, PhoneNumber);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(PhoneErrors.CountryCodeNullOrEmpty,result.Error);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreatePhoneValueObject_Should_ReturnError_WhenNumberIsNullOrEmpty(string? number)
    {
        //Arrange

        //Act
        var result = Phone.Create(CountryCode, number);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(PhoneErrors.NumberNullOrEmpty, result.Error);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenInvalidNumberLength()
    {
        //Arrange
        string number = "55555";
        //Act
        var result = Phone.Create(CountryCode, number);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(PhoneErrors.InvalidNumber, result.Error);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenInvalidCountryCodeLength()
    {
        //Arrange
        string countryCode = "9";
        //Act
        var result = Phone.Create(countryCode, PhoneNumber);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(PhoneErrors.InvalidCountryCode, result.Error);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenNumberNotStarts5()
    {
        //Arrange
        string phoneNumber = "4555555555";
        //Act
        var result = Phone.Create(CountryCode, phoneNumber);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(PhoneErrors.ShouldBeStartsFive, result.Error);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_Succeed_WithValidInputs()
    {
        //Arrange
        
        //Act
        var result = Phone.Create(CountryCode, PhoneNumber);
        
        //Assert
        Assert.NotNull(result);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        Assert.Equal(CountryCode, result.Data.CountryCode);
        Assert.Equal(PhoneNumber, result.Data.Number);
    }
}