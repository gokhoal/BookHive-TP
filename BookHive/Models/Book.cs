using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.Models;

public class Book
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(200)] public string Title { get; set; }
    [Required, Length(13, 13)] public string ISBN { get; set; }
    [MaxLength(3000)] public string? Summary { get; set; }
    [Required] public int PageCount { get; set; }
    [Required] public DateOnly PublishedDate { get; set; }
    [Required, MaxLength(50)] public string Genre { get; set; }
    
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public List<Loan>? Loan { get; set; }
    public List<Review>? Review { get; set; }
    
}