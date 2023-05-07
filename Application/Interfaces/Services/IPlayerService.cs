using Application.DTO;

namespace Application.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<CardDTO> DrawACardFromDeck(string roomId, string connectionId);
        Task<PlayerDTO> ThrowACardToStack(string roomId, string connectionId, int cardId);
        Task<List<CardDTO>> TakeCardsFromStack(string roomId, string connectionId);
    }
}
