using Microsoft.Extensions.Options;
using OnionArchitecture.Application.Abstractions.Repositories;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Auth.Requests;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Application.Options;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Errors.Entity;
using OnionArchitecture.Shared;
using OnionArchitecture.Domain.ValueObjects;

namespace OnionArchitecture.Persistence.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;
    
    public UserService(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }
    
    public async Task<Result<TokenDto>> RegisterAsync(UserRegisterRequest request)
    {
        User? user = await _unitOfWork.UserReadRepository.
            GetByPhoneOrEmailAsync($"{request.CountryCode}{request.Phone}", request.Email);

        if (user != null)
        {
            if ($"{request.CountryCode}{request.Phone}" == user.Phone.Value)
            {
                return Result<TokenDto>.Failure(UserErrors.AlreadyExistsPhone);
            }
            if (request.Email == user.Email.Value)
            {
                return Result<TokenDto>.Failure(UserErrors.AlreadyExistsEmail);
            }
            return Result<TokenDto>.Failure(UserErrors.AlreadyExists);
        }
        
        if (request.Password != request.PasswordConfirm)
        {
            return Result<TokenDto>.Failure(UserErrors.PasswordDoesntMatch);
        }
        
        var nameResult = Name.Create(request.FirstName, request.LastName);
        if (nameResult.IsFailure)
        {
            return Result<TokenDto>.Failure(nameResult.Error!);
        }

        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result<TokenDto>.Failure(emailResult.Error!);
        }

        var phoneResult = Phone.Create(request.CountryCode, request.Phone);
        if (phoneResult.IsFailure)
        {
            return Result<TokenDto>.Failure(phoneResult.Error!);
        }
        
        var userResult = User.Create(
            nameResult.Data!,
            emailResult.Data!,
            phoneResult.Data!,
            request.Password,
            _passwordHasher.Hash(request.Password));

        if (userResult.IsFailure)
        {
            return Result<TokenDto>.Failure(userResult.Error!);
        }

        Role? defaultRole = await _unitOfWork.RoleReadRepository.GetDefaultRoleAsync();
        if (defaultRole == null)
        {
            return Result<TokenDto>.Failure(RoleErrors.DefaultRoleNotFound);
        }
        
        user = userResult.Data;
        user.AddRole(defaultRole);

        var accessTokenResult = _tokenService.CreateAccessToken(user);
        var refreshTokenResult = _tokenService.GenerateRefreshToken();
        
        user.UpdateRefreshToken(refreshTokenResult.Data!, _jwtOptions.RefreshTokenExpireTimeSecond);
        user.UpdateLastLoginDate();
        
        await _unitOfWork.UserWriteRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return Result<TokenDto>.Success(new(accessTokenResult.Data, refreshTokenResult.Data));

    }

    public async Task<Result<TokenDto>> LoginAsync(string phoneOrEmail, string password)
    {
        User? user = await _unitOfWork.UserReadRepository.
            GetByPhoneOrEmailAsync(phoneOrEmail);

        if (user == null)
        {
            return Result<TokenDto>.Failure(UserErrors.NotFound);
        }

        if (!_passwordHasher.Verify(user.Password, password))
        {
            return Result<TokenDto>.Failure(UserErrors.WrongUsernameOrPassword);
        }
        
        var accessTokenResult = _tokenService.CreateAccessToken(user);

        if (user.RefreshTokenExpireAt <= DateTime.UtcNow)
        {
            var refreshTokenResult = _tokenService.GenerateRefreshToken();
            user.UpdateRefreshToken(refreshTokenResult.Data!, _jwtOptions.RefreshTokenExpireTimeSecond);
        }
        user.UpdateLastLoginDate();

        await _unitOfWork.CompleteAsync();

        return Result<TokenDto>.Success(new(accessTokenResult.Data!, user.RefreshToken));
    }

    public async Task<Result<TokenDto>> RefreshToken(string accessToken, string refreshToken)
    {
        var userIdResult = _tokenService.GetUserIdFromJwtToken(accessToken);
        if (userIdResult.IsFailure)
        {
            return Result<TokenDto>.Failure(userIdResult.Error!);
        }

        Guid.TryParse(userIdResult.Data!, out var userId);

        var user = await _unitOfWork.UserReadRepository.GetByIdWithRolesAsync(userId);
        if (user == null)
        {
            return Result<TokenDto>.Failure(UserErrors.NotFound);
        }

        if (user.RefreshToken != refreshToken ||
            user.RefreshTokenExpireAt < DateTime.UtcNow)
        {
            return Result<TokenDto>.Failure(UserErrors.SessionExpired);
        }

        var accessTokenResult =  _tokenService.CreateAccessToken(user);
        var refreshTokenResult = _tokenService.GenerateRefreshToken();
        
        user.UpdateRefreshToken(refreshTokenResult.Data!, _jwtOptions.RefreshTokenExpireTimeSecond);
        user.UpdateLastLoginDate();
        
        await _unitOfWork.CompleteAsync();
        
        return Result<TokenDto>.Success(new(accessTokenResult.Data!, user.RefreshToken));
    }
}