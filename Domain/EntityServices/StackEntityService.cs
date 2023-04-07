using Domain.EntityInterfaces;
using Domain.Entities.CardEntities;
using Domain.Enums;

namespace Domain.EntityServices
{
    public class StackEntityService : IStackEntityService
    {
        private readonly Stack _stack;

        public StackEntityService(Stack stack)
        {
            _stack = stack;
        }
        public void stackDrawingMode()
        {
            var firstCard = _stack.Cards[0];
            var desiredCard = _stack.Cards.First(x => x.Value == CardValue.Nine && x.Suit == CardSuit.Hearts);

            if (firstCard == desiredCard)
            {
                _stack.Mode = StackDrawingMode.ThreeCards;
            }
            else
            {
                _stack.Mode = StackDrawingMode.All;
            }
        }
    }
}
