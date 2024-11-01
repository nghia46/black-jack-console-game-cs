namespace black_jack;

public class Player
{
    public List<Card> Hand { get; private set; }
    public bool HasStood { get; set; } = false;

    public Player()
    {
        Hand = [];
    }
    // Add a card to the player's hand
    // For example, if the player draws a 2 of hearts, you can call player.AddCard(new Card("2", "Hearts"));
    public void AddCard(Card card)
    {
        Hand.Add(card);
    }

    // Calculate the score of the player's hand returning the total score
    // Calculate the score of the player's hand, returning both the maximum and minimum possible scores
    public (int maxScore, int minScore) CalculateScore()
    {
        int score = 0;
        int aceCount = 0;

        // Calculate the score of the player's hand and count the number of aces
        foreach (var card in Hand)
        {
            score += card.GetValue();
            if (card.Rank == "A") aceCount++;
        }

        // Calculate the minimum score by counting all aces as 1
        int minScore = score - (aceCount * 10); // Each ace counted as 1 instead of 11

        // The max score will be the score with all aces treated as 11 if possible
        int maxScore = score;

        // If max score is greater than 21, we reduce the value of each ace to 1 until it's <= 21
        while (maxScore > 21 && aceCount > 0)
        {
            maxScore -= 10;
            aceCount--;
        }

        return (maxScore, minScore);
    }

    // Check if the player has busted exceeding 21
    public bool IsBust()
    {
        return CalculateScore().maxScore > 21;
    }

    public bool IsTwentyOne()
    {
        return CalculateScore().maxScore == 21;
    }
    // Check if the player has blackjack with an ace and a 10-value card
    public bool IsBlackJack()
    {
        return Hand.Count == 2 && CalculateScore().maxScore == 21;
    }
    
    public bool IsFiveCardCharlie()
    {
        return Hand.Count == 5 && CalculateScore().maxScore <= 21;
    }
}
