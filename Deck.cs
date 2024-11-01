namespace black_jack;

public class Deck
{
    private readonly List<Card> cards;
    private readonly Random rng = new();

    // inintialize the deck with 52 cards and shuffle them
    public Deck()
    {
        cards = [];
        string[] suits = ["Hearts", "Diamonds", "Clubs", "Spades"];
        string[] ranks = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                cards.Add(new Card(rank, suit));
            }
        }
        Shuffle();
    }
    // shuffle the deck with Fisher-Yates algorithm
    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (cards[n], cards[k]) = (cards[k], cards[n]);
        }
    }
    // draw a card from the deck
    public Card DrawCard()
    {
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}
