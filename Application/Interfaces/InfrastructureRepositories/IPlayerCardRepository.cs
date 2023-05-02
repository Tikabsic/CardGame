using Domain.Entities.CardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IPlayerCardRepository
    {
        Task AddCard(PlayerCard card);
        Task RemoveCard(PlayerCard card);
    }
}
