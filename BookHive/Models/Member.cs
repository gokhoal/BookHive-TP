using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.Models;

public class Member
{
    [Key] public int Id { get; set; }
    [Required] public string Email {get; set;}
    [Required, MaxLength(100)] public string FirstName {get; set;}
    [Required, MaxLength(100)] public string LastName {get; set;}
    [Required] public DateOnly MembershipDate {get; set;} 
    public bool IsActive {get; set;}
    public List<Loan>? Loans { get; set; }
    public List<Review>? Reviews { get; set; }
    
}