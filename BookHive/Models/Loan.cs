using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.Models;

public class Loan
{
    [Key] public int Id { get; set; }
    [Required] public DateOnly LoanDate { get; set; }
    [Required] public DateOnly DueDate { get; set; } 
    public DateOnly? ReturnDate { get; set; }
    public Book? Book { get; set; }
    public Member? Member { get; set; }
    
    [Required] public int BookId { get; set; }
    [Required] public int MemberId { get; set; }
}