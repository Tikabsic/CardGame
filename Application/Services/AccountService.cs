using Application.Data;
using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger _logger;
        private readonly IAppDbContext _dbContext;
        public AccountService(IPasswordHasher passwordHasher, IAccountRepository accountRepository, ILogger<AccountService> logger, IAppDbContext dbContext)
        {
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task RegisterUser(RegisterUserDTO dto)
        {

            var hashedPassword = _passwordHasher.Hash(dto.Password);

            var newUser = new User()
                {
                    Name = dto.Name,
                    Password = hashedPassword
                };

                await _accountRepository.AddUser(newUser);
            
        }
    }
}
