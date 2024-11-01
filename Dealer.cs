namespace black_jack;

public class Dealer : Player
{
    // Check if the dealer should draw new card if the score is less than 17
    // this automatically makes the dealer draw a new card if the score is less than 17
    public bool ShouldDrawCard()
    {
        return CalculateScore().maxScore < 17;
    }
}
