using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [NotMapped]
    public class Stack
    {
        public List<Card> Cards { get; set;}
    }
}
