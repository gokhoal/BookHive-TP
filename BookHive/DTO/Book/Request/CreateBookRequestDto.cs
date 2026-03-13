using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Book.Request;

public class CreateBookRequestDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string? Summary { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedDate { get; set; }
    public string Genre { get; set; }    
    public int AuthorId { get; set; }
    
    public Navigation Author { get; set; }
    
    public List<Navigation> Loans { get; set; }
    public List<Navigation> Reviews { get; set; }
    

}