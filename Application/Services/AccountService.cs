﻿using Application.Authentication;
using Application.Data;
using Application.DTO;
using Application.DTO.Validators;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities.PlayerEntities;
using AutoMapper;
using Newtonsoft.Json;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IAppDbContext _dbContext;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AccountService(IPasswordHasher passwordHasher, IAccountRepository accountRepository, ILogger<AccountService> logger, IAppDbContext dbContext, AuthenticationSettings authenticationSettings, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
            _logger = logger;
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<string> GetUserInfo()
        {
            var user = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            var claims = user.Principal.Claims.Select(c => new { Type = c.Type, Value = c.Value });

            var userInfo = JsonConvert.SerializeObject(claims, Formatting.Indented);

            return userInfo;
        }

        public async Task<bool> IsUserNameTaken(string name)
        {
            var result = await _userRepository.IsUserNameTaken(name);

            if (result)
            {
                throw new BadRequestException("Username already taken.");
            }

            return result;
        }

        public async Task<List<FluentValidation.Results.ValidationFailure>> RegisterUser(RegisterUserDTO dto)
        {
            var validator = new RegisterUserValidator(_dbContext);
            var result = validator.Validate(dto);

            if (result.IsValid)
            {
                var hashedPassword = _passwordHasher.Hash(dto.Password);

                var newUser = new User()
                {
                    Name = dto.Name,
                    Password = hashedPassword
                };

                await _accountRepository.AddUser(newUser);
            }

            return result.Errors;
        }

        public async Task<string> Login(LoginUserDTO dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == dto.Name);

            if (user is null)
            {
                throw new BadRequestException("Invalid name or password");
            }

            var result = _passwordHasher.Verify(user.Password, dto.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid name or password");
            }

            var userScoreClaim = new Claim("Id", user.UserScore.ToString());
            var userScoreClaim2 = new Claim("Name", user.UserScore.ToString());
            var userScoreClaim3 = new Claim("UserScore", user.UserScore.ToString());
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", $"{user.Name}"),
                new Claim("UserScore", user.UserScore.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JWTKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JWTExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JWTIssuer, _authenticationSettings.JWTIssuer, claims, expires: expires, signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token).ToString();
        }
       
    }
}
