namespace OnionArchitecture.Application.DTOs.Auth.Requests;

public class UserRegisterRequest
{
    public UserRegisterRequest(string firstName, string lastName, string email, string countryCode, string phone, string password, string passwordConfirm)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CountryCode = countryCode;
        Phone = phone;
        Password = password;
        PasswordConfirm = passwordConfirm;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CountryCode { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}