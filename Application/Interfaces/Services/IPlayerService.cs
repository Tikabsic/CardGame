using Application.DTO;

namespace Application.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<PlayerDTO> DrawACardFromDeck(string roomId, string connectionId);
        Task<PlayerDTO> ThrowACardToStack(string roomId, string connectionId, int cardId);
        Task<PlayerDTO> TakeCardsFromStack(string roomId, string connectionId);
    }
}
