﻿using Application.DTO;
using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly IPlayerCardRepository _playerCardRepository;

        public PlayerService(
            IRoomRepository roomRepository,
            IPlayerRepository playerRepository,
            IMapper mapper,
            IPlayerCardRepository playerCardRepository
            )
        {
            _playerRepository = playerRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _playerCardRepository = playerCardRepository;
        }

        private async Task TakeCards(string roomId, string connectionId ,int amountOfCards)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var caller = await _playerRepository.GetPlayerAsync(connectionId);
            var stack = room.Stack;

            var cards = stack.Cards.TakeLast(amountOfCards);

            foreach (var card in cards)
            {
                var playerCard = new PlayerCard()
                {
                    Card = card.Card,
                    CardId = card.CardId,
                    Player = caller,
                    PlayerId = caller.Id
                };

                caller.Hand.Add(playerCard);
                stack.Cards.Remove(card);
                await _playerCardRepository.AddCard(playerCard);
            }

            await _roomRepository.UpdateRoomAsync(room);
            await _playerRepository.UpdatePlayerAsync(caller);
        }

        public async Task<PlayerDTO> DrawACardFromDeck(string roomId, string connectionId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var caller = await _playerRepository.GetPlayerAsync(connectionId);
            var lastDeckCard = room.Deck.Cards.Last();

            var playerCard = new PlayerCard()
            {
                Card = lastDeckCard.Card,
                CardId = lastDeckCard.CardId,
                Player = caller,
                PlayerId = caller.Id
            };

            room.Deck.Cards.Remove(lastDeckCard);
            caller.Hand.Add(playerCard);

            caller.IsCardDrewFromDeck = true;
            await _playerRepository.UpdatePlayerAsync(caller);

            await _roomRepository.UpdateRoomAsync(room);

            var playerDTO = _mapper.Map<PlayerDTO>(caller);



            return playerDTO;
        }

        public async Task<PlayerDTO> ThrowACardToStack(string roomId, string connectionId, int cardId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var caller = await _playerRepository.GetPlayerAsync(connectionId);
            var card = caller.Hand.First(c => c.Card.Id == cardId);

            var stackCard = new StackCard()
            {
                Card = card.Card,
                CardId = card.CardId,
                Stack = room.Stack,
                StackId = room.Stack.Id
            };

            room.Stack.Cards.Add(stackCard);
            caller.Hand.Remove(card);
            caller.IsCardThrownToStack = true;

            await _playerRepository.UpdatePlayerAsync(caller);
            await _roomRepository.UpdateRoomAsync(room);

            var playerDTO = _mapper.Map<PlayerDTO>(caller);

            return playerDTO;
        }

        public async Task<PlayerDTO> TakeCardsFromStack(string roomId, string connectionId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var caller = await _playerRepository.GetPlayerAsync(connectionId);
            var stack = room.Stack;

            

            if (stack.Mode == StackDrawingMode.ThreeCards && caller.IsCardDrewFromDeck)
            {
                await TakeCards(roomId, connectionId, 2);
            }

            if (stack.Mode == StackDrawingMode.ThreeCards && !caller.IsCardDrewFromDeck)
            {
                await TakeCards(roomId, connectionId, 3);
            }

            if (stack.Mode == StackDrawingMode.All)
            {
                var allCards = stack.Cards.ToList();

                foreach (var card in allCards)
                {
                    var playerCard = new PlayerCard()
                    {
                        Card = card.Card,
                        CardId = card.CardId,
                        Player = caller,
                        PlayerId = caller.Id
                    };

                    caller.Hand.Add(playerCard);
                    stack.Cards.Remove(card);
                    await _playerCardRepository.AddCard(playerCard);
                }
                caller.IsCardsDrewFromStack = true;

                await _roomRepository.UpdateRoomAsync(room);
                await _playerRepository.UpdatePlayerAsync(caller);
            }

            var playerDTO = _mapper.Map<PlayerDTO>(caller);

            return playerDTO;
        }
    }
}
