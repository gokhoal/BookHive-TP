using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.Models;

public class Review
{
    [Key] public int Id { get; set; }
    [Required] public int Rating { get; set; }
    [Required, MinLength(1), MaxLength(5)] public string? Comment { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
    
    public List<Book>? Book { get; set; }
    public List<Member>? Member { get; set; }
    
    public int BookId { get; set; }
    public int MemberId { get; set; }
}
