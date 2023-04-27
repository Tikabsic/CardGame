namespace Domain.Entities.PlayerEntities
{
    public class PlayerStates
    {
        public bool IsCardDrewFromDeck { get; set; } = false;
        public bool IsCardThrownToStack { get; set; } = false;
        public bool IsPlayerAbleToThrowACard { get; set; }
    }
}
