using Domain.Entities.CardEntities;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.EntityServices
{
    internal class StackEntityService : IStackEntityService
    {
        public void stackDrawingMode(Stack stack)
        {
            if (stack.Cards != null && stack.Cards.Any())
            {
                var firstCard = stack.Cards.ToArray()[0];

                var desiredCard = stack.Cards.First(x => x.Value == CardValue.Nine && x.Suit == CardSuit.Hearts);

                if (firstCard == desiredCard)
                {
                    stack.Mode = StackDrawingMode.ThreeCards;
                }
            }
            else
            {
                stack.Mode = StackDrawingMode.All;
            }
        }
    }
}
