using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Review.Response;

public class GetReviewDetailsDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Navigation Book { get; set; }
    public Navigation Member { get; set; }
}