# Dikky

Dikky is a multiplayer web application developed in C# using ASP.NET Core and SignalR.

## Game Overview

Card Game is a turn-based game that is played with a standard deck of 52 cards. The objective of the game is to be the first player to empty their hand of cards by playing them to a central discard pile.

In each round of the game, players take turns playing one or more of the following actions:

- Draw a card from the top of the draw pile.
- Draw a card from the top of the discard pile.
- Play one or more cards from their hand to the discard pile.

A player's turn ends when they have played one or more cards to the discard pile or when they have drawn a card from either pile.

The game continues until one player has emptied their hand of cards, at which point that player is declared the winner.

## Installation and Usage

To install and use Card Game, follow these steps:

1. Clone the repository: `git clone https://github.com/Tikabsic/CardGame.git`
2. Open the solution file (CardGame.sln) in Visual Studio.
3. Build and run the solution.
4. Clone and run the web client from my other repository, which is mentioned below.
5. Enter a username and create or join a game.
6. Wait for other players to join the game.
7. Play the game!

## Client

The client for the game can be found at https://github.com/Tikabsic/CardGameClient.

## Features

The game currently supports the following features:

- Multiplayer games with up to four players.
- Real-time communication between clients and the server using SignalR.
- Randomized card distribution.

## Tech

<a href="https://dotnet.microsoft.com/en-us/">.NET 7 </a>

## Contributing

If you would like to contribute to the development of Card Game, please feel free to submit pull requests or open issues on the GitHub repository.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
