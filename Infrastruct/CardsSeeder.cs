
using Domain.Entities.CardEntities;
using Domain.Enums;
using Infrastruct.Persistence;

namespace Infrastruct
{
    internal class CardsSeeder
    {
        private readonly AppDbContext _dbContext;
        public CardsSeeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedCards()
        {
            for (int i = (int)CardValue.Nine; i <= (int)CardValue.Ace; i++)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    _dbContext.Cards.Add(new Card { Value = (CardValue)i, Suit = suit });
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
