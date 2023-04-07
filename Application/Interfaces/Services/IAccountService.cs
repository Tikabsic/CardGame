using Application.DTO;
using Domain.Entities.PlayerEntities;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> GetUserInfo();
        Task<List<FluentValidation.Results.ValidationFailure>> RegisterUser(RegisterUserDTO dto);
        Task<string> GenerateJWT(LoginUserDTO dto);
    }
}
