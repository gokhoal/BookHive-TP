using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.Models;

public class Author
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public string FirstName { get; set; }
    [Required, MaxLength(100)] public string LastName { get; set; }
    [MaxLength(2000)] public string? Biography { get; set; }
    [Required] public DateOnly BirthDate { get; set; }
    [Required, MaxLength(60)] public string Nationality { get; set; }
    
    public List<Book>? Books { get; set; }
}