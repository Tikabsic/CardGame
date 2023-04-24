
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;

namespace Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public Player Author { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Room Room { get; set; }
        public string RoomId { get; set; }
        private DateTime _date;
        public string PlayerMessage { get; set; }

        public DateTime Date
        {
            get => _date;
            set => _date = value.Date + value.TimeOfDay;
        }
    }
}
