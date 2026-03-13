using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Book.Response;

public class GetBookDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string? Summary { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedDate { get; set; }
    public string Genre { get; set; }
    public Models.Author Author { get; set; }
    
    public List<Models.Loan> Loans { get; set; }
    public List<Models.Review> Reviews { get; set; }
    
    public int AuthorId { get; set; }
}