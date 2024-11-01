namespace black_jack;

using System;

public class Game
{
    private readonly Deck deck;
    private readonly Player player;
    private readonly Dealer dealer;

    public Game()
    {
        deck = new Deck(); // The deck is shuffled in its constructor
        player = new Player();
        dealer = new Dealer();
    }

    public void Start()
    {
        // Deal two cards to the player and two cards to the dealer
        player.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());

        // Display the welcome message
        Console.WriteLine("Welcome to Blackjack!");

        // The game loop
        while (true)
        {
            // Display the player's hand and score
            DisplayHand("Your hand", player);
            Console.WriteLine($"Your score: {player.CalculateScore()}");

            // Check if the player has Blackjack or Five-Card Charlie, announce but do not end the game
            if (player.IsBlackJack())
            {
                Console.WriteLine("Blackjack! You might win, but let's see the dealer's turn.");
                player.HasStood = true;
            }
            else if (player.IsFiveCardCharlie())
            {
                Console.WriteLine("Five-Card Charlie! You might win, but let's see the dealer's turn.");
                player.HasStood = true;
            }

            // Check if the player has a hard 21 (both min and max score are 21)
            var playerScore = player.CalculateScore();
            bool hasHard21 = playerScore.minScore == 21 && playerScore.maxScore == 21;

            // Automatically stand if the player has a hard 21
            if (hasHard21)
            {
                Console.WriteLine("You have a hard 21! The dealer will now take their turn.");
                player.HasStood = true;
            }

            // Check if the player has busted
            if (player.IsBust())
            {
                Console.WriteLine("Bust! You lose.");
                RevealDealerHand();
                return;
            }

            // Player's choice to hit or stand if they haven’t stood yet
            if (!player.HasStood)
            {
                Console.Write("Do you want to hit or stand? (h/s): ");
                string choice = Console.ReadLine() ?? "";
                if (choice.Equals("h", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Draw a new card if the player chooses to hit
                    player.AddCard(deck.DrawCard());
                }
                else if (choice.Equals("s", StringComparison.CurrentCultureIgnoreCase))
                {
                    player.HasStood = true;
                }
            }

            // Dealer's turn once the player stands or reaches a winning condition
            if (player.HasStood)
            {
                while (dealer.ShouldDrawCard())
                {
                    dealer.AddCard(deck.DrawCard());
                }

                // Display dealer's hand and determine the winner
                RevealDealerHand();
                DetermineWinner();
                break;
            }
        }
    }

    // Display the hand of the player or the dealer
    private static void DisplayHand(string name, Player player)
    {
        Console.WriteLine($"{name}:");
        foreach (var card in player.Hand)
            Console.WriteLine($" - {card.Rank} of {card.Suit}");
    }

    // Reveal the dealer's hand and score
    private void RevealDealerHand()
    {
        DisplayHand("Dealer's hand", dealer);
        Console.WriteLine($"Dealer's score: {dealer.CalculateScore()}");
    }

    // Determine the winner of the game
    private void DetermineWinner()
    {
        // Check for both having Blackjack
        if (player.IsBlackJack() && dealer.IsBlackJack())
        {
            Console.WriteLine("Both you and the dealer have Blackjack! It's a tie!");
            return;
        }
        else if (player.IsBlackJack())
        {
            Console.WriteLine("Blackjack! You win!");
            return;
        }
        else if (dealer.IsBlackJack())
        {
            Console.WriteLine("Dealer has Blackjack. Dealer wins.");
            return;
        }

        // Check for both having Five-Card Charlie
        if (player.IsFiveCardCharlie() && dealer.IsFiveCardCharlie())
        {
            Console.WriteLine("Both you and the dealer have Five-Card Charlie! It's a tie!");
            return;
        }
        else if (player.IsFiveCardCharlie())
        {
            Console.WriteLine("Five-Card Charlie! You win!");
            return;
        }
        else if (dealer.IsFiveCardCharlie())
        {
            Console.WriteLine("Dealer has Five-Card Charlie. Dealer wins.");
            return;
        }

        // Standard score comparison
        int playerScore = player.CalculateScore().maxScore > 0 ? player.CalculateScore().maxScore : player.CalculateScore().minScore;
        int dealerScore = dealer.CalculateScore().maxScore > 0 ? dealer.CalculateScore().maxScore : dealer.CalculateScore().minScore;

        if (dealerScore > 21 || playerScore > dealerScore)
            Console.WriteLine("You win!");
        else if (playerScore < dealerScore)
            Console.WriteLine("Dealer wins.");
        else
            Console.WriteLine("It's a tie!");
    }
}
