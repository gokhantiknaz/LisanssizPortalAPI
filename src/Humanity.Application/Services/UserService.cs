﻿using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests;
using Humanity.Application.Repositories;
using Humanity.Domain.Core.Models;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Services
{
    public class UserService : IUserService
    {

        private readonly ILoggerService _loggerService;
        private readonly JwtSettings jwtSettings;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpService httpService;

        public UserService(IOptions<JwtSettings> jwtSettingsOptions,
        UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork, IHttpService httpService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpService = httpService;
            jwtSettings = jwtSettingsOptions.Value;
        }

        public Task<CreateUserRes> CreateUser(CreateUserReq req)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (!signInResult.Succeeded)
            {
                return null;
            }

            var user = await userManager.FindByNameAsync(request.UserName);
            await userManager.UpdateSecurityStampAsync(user);

            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, request.UserName),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new(ClaimTypes.Email, user.Email),
            //new(ClaimTypes.GroupSid, user.Id?.ToString() ?? string.Empty),
            new(ClaimTypes.SerialNumber, user.SecurityStamp.ToString())
        }.Union(userRoles.Select(role => new Claim(ClaimTypes.Role, role))).ToList();

            var loginResponse = CreateToken(claims);

            user.RefreshToken = loginResponse.RefreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationMinutes);

            await userManager.UpdateAsync(user);
            return loginResponse;
        }

        public async Task<AuthResponse> ImpersonateAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.LockoutEnd.GetValueOrDefault() > DateTimeOffset.UtcNow)
            {
                return null;
            }

            await userManager.UpdateSecurityStampAsync(user);
            var identity = httpService.GetIdentity();

            UpdateClaim(ClaimTypes.NameIdentifier, user.Id.ToString());
            UpdateClaim(ClaimTypes.Name, user.UserName);
            UpdateClaim(ClaimTypes.GivenName, user.FirstName);
            UpdateClaim(ClaimTypes.Surname, user.LastName ?? string.Empty);
            UpdateClaim(ClaimTypes.Email, user.Email);
            //UpdateClaim(ClaimTypes.GroupSid, user.id?.ToString() ?? string.Empty);
            UpdateClaim(ClaimTypes.SerialNumber, user.SecurityStamp.ToString());

            var loginResponse = CreateToken(identity.Claims.ToList());

            user.RefreshToken = loginResponse.RefreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationMinutes);

            await userManager.UpdateAsync(user);

            return loginResponse;

            void UpdateClaim(string type, string value)
            {
                var existingClaim = identity.FindFirst(type);
                if (existingClaim is not null)
                {
                    identity.RemoveClaim(existingClaim);
                }

                identity.AddClaim(new Claim(type, value));
            }
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var user = ValidateAccessToken(request.AccessToken);
            if (user != null)
            {
                var userId = user.GetId();
                var dbUser = await userManager.FindByIdAsync(userId.ToString());

                if (dbUser?.RefreshToken == null || dbUser?.RefreshTokenExpirationDate < DateTime.UtcNow || dbUser?.RefreshToken != request.RefreshToken)
                {
                    return null;
                }

                var loginResponse = CreateToken(user.Claims.ToList());

                dbUser.RefreshToken = loginResponse.RefreshToken;
                dbUser.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationMinutes);

                await userManager.UpdateAsync(dbUser);

                return loginResponse;
            }

            return null;
        }

        private AuthResponse CreateToken(IList<Claim> claims)
        {
            var audienceClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Aud);
            claims.Remove(audienceClaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claims,
                DateTime.UtcNow, DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes), signingCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var response = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken()
            };

            return response;

            static string GenerateRefreshToken()
            {
                var randomNumber = new byte[256];
                using var generator = RandomNumberGenerator.Create();
                generator.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal ValidateAccessToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var user = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
                if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg == SecurityAlgorithms.HmacSha256)
                {
                    return user;
                }
            }
            catch
            {
            }

            return null;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                RefreshToken = ""
            };

            try
            {
                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, RoleNames.User);
                }
                var response = new RegisterResponse
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.Select(e => e.Description)
                };
                return response;
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Succeeded = false,
                    Errors = new List<string>() { ex.Message }
                };
            }

        }


    }
}
