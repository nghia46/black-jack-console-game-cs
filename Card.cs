namespace black_jack;

public class Card(string rank, string suit)
{
    public string Rank { get; } = rank;
    public string Suit { get; } = suit;

    // Get the value of the card based on its rank (2-10, J, Q, K, A) ace is 11 or 1
    public int GetValue(){
        return Rank switch
        {
            "J" or "Q" or "K" => 10,
            "A" => 11,
            _ => int.Parse(Rank)
        };
    }
}
