using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class PhoneTests
{
    private const string _countryCode = "90";
    private const string _phoneNumber = "5555555555";
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenCountryCodeIsNull()
    {
        //Arrange

        //Act
        var result = Phone.Create(null, _phoneNumber);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(result?.Error?.Message, PhoneErrors.CountryCodeNullOrEmpty.Message);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenNumberIsNull()
    {
        //Arrange

        //Act
        var result = Phone.Create(_countryCode, null);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(result?.Error?.Message, PhoneErrors.NumberNullOrEmpty.Message);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenInvalidNumberLength()
    {
        //Arrange
        string number = "55555";
        //Act
        var result = Phone.Create(_countryCode, number);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(result?.Error?.Message, PhoneErrors.InvalidNumber.Message);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenInvalidCountryCodeLength()
    {
        //Arrange
        string countryCode = "9";
        //Act
        var result = Phone.Create(countryCode, _phoneNumber);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(result?.Error?.Message, PhoneErrors.InvalidCountryCode.Message);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_ReturnError_WhenNumberNotStarts5()
    {
        //Arrange
        string phoneNumber = "4555555555";
        //Act
        var result = Phone.Create(_countryCode, phoneNumber);
        
        //Assert
        Assert.NotNull(result?.Error);
        Assert.Equal(result?.Error?.Message, PhoneErrors.ShouldBeStartsFive.Message);
    }
    
    [Fact]
    public void CreatePhoneValueObject_Should_Succeed_WithValidInputs()
    {
        //Arrange
        
        //Act
        var result = Phone.Create(_countryCode, _phoneNumber);
        
        //Assert
        Assert.NotNull(result);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        Assert.Equal(_countryCode, result.Data.CountryCode);
        Assert.Equal(_phoneNumber, result.Data.Number);
    }
}