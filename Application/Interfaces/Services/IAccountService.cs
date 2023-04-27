using Application.DTO;
using Domain.Entities.PlayerEntities;

namespace Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> isPlayerOnline(LoginUserDTO dto);
        Task<Player> GetPlayer();
        Task<List<FluentValidation.Results.ValidationFailure>> RegisterUser(RegisterUserDTO dto);
        Task<string> Login(LoginUserDTO dto);
        Task<bool> IsUserNameTaken(string name);

    }
}
