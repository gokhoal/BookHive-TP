using BookHive.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHive;

public class BookHiveDbContext : DbContext
{

//Connexion à la base de données et création des différentes tables

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Infos de connexion à la base de données
        string connectionString = 
            "Server=romaric-thibault.fr;" +
            "Database=gabriel_BookHive;" +
            "User Id=gabriel;" +
            "Password=Onto9-Cage-Afflicted;" +
            "TrustServerCertificate=true;";
        
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
