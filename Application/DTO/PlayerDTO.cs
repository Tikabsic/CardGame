using Domain.Enums;

namespace Application.DTO
{
    public class PlayerDTO
    {
        public int ListIndex { get; set; }
        public Roles Role { get; set; }
        public string Name { get; set; }
        public int UserScore { get; set; }
        public List<CardDTO> Hand { get; set; }
    }
}
