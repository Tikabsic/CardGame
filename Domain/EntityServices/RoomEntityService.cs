using Domain.Interfaces;

namespace Domain.EntityServices
{
    internal class RoomEntityService : IRoomEntityService
    {

        public string RoomIdGenerator()
        {
            var numberOfDigits = 6;
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rng = new Random();
            var id = new char[numberOfDigits];

            for (int i = 0; i < numberOfDigits; i++)
            {
                id[i] = chars[rng.Next(chars.Length)];
            }

            return new string(id);
        }

    }
}
