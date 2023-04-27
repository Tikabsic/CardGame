namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public int UserScore { get; set; } = 0;
    }
}
