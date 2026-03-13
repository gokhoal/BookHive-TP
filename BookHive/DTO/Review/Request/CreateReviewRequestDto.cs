using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Review.Request;

public class CreateReviewRequestDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Models.Book Book { get; set; }
    public Models.Member Member { get; set; }
}