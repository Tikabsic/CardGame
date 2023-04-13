using Domain.EntityInterfaces;
using Domain.Entities.CardEntities;
using Domain.Enums;

namespace Domain.EntityServices
{
    public class StackEntityService : IStackEntityService
    {

        public void stackDrawingMode(Stack stack)
        {
            var firstCard = stack.Cards.ToArray()[0];

            var desiredCard = stack.Cards.First(x => x.Value == CardValue.Nine && x.Suit == CardSuit.Hearts);

            if (firstCard == desiredCard)
            {
                stack.Mode = StackDrawingMode.ThreeCards;
            }
            else
            {
                stack.Mode = StackDrawingMode.All;
            }
        }
    }
}
