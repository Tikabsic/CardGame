using Domain.Enums;

namespace Application.DTO
{
    internal class PlayerDTO
    {
        public Roles Role { get; set; }
        public string Name { get; set; }
        public int UserScore { get; set; }
        public List<CardDTO> Hand { get; set; }
    }
}
