using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityInterfaces
{
    public interface IRoomEntityService
    {
        Room CreateRoom(Player player);
    }
}
