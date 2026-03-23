namespace BookHive.DTO.Book.Request;

public class GetBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
    public string AuthorFullName { get; set; }
    public string AuthorNationality { get; set; }
}