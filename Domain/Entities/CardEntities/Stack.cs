using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    [NotMapped]
    public class Stack
    {
        public List<Card> Cards { get; set; }
        public StackDrawingMode Mode { get; set; }

    }
}
