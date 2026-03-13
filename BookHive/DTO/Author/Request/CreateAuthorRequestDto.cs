using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Author.Request;

public class CreateAuthorRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Biography { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Nationality { get; set; }
    public List<Navigation> Books { get; set; }
}