namespace BookHive.DTO.Review.Request;

public class GetReviewDto
{
    public int Id { get; set; }
        
    public Models.Book Book { get; set; }
    public Models.Member Member { get; set; }
}