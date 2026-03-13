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
        // ── Authors ──────────────────────────────────────────────────────────────
    modelBuilder.Entity<Author>().HasData(
        new Author
        {
            Id = 1,
            FirstName = "Victor",
            LastName = "Hugo",
            Biography = "Victor Hugo est l'un des plus importants écrivains romantiques français, auteur de Notre-Dame de Paris et Les Misérables.",
            BirthDate = new DateOnly(1802, 2, 26),
            Nationality = "Française"
        },
        new Author
        {
            Id = 2,
            FirstName = "Albert",
            LastName = "Camus",
            Biography = "Albert Camus est un romancier, essayiste et dramaturge français, lauréat du prix Nobel de littérature en 1957.",
            BirthDate = new DateOnly(1913, 11, 7),
            Nationality = "Française"
        },
        new Author
        {
            Id = 3,
            FirstName = "Jane",
            LastName = "Austen",
            Biography = "Jane Austen est une romancière anglaise dont les œuvres critiquent avec ironie la société britannique de la fin du XVIIIe siècle.",
            BirthDate = new DateOnly(1775, 12, 16),
            Nationality = "Britannique"
        }
    );

    // ── Books ─────────────────────────────────────────────────────────────────
    modelBuilder.Entity<Book>().HasData(
        new Book
        {
            Id = 1,
            Title = "Les Misérables",
            ISBN = "9782070409228",
            Summary = "L'histoire de Jean Valjean, un ancien forçat qui cherche à se racheter dans une France du XIXe siècle tourmentée.",
            PageCount = 1900,
            PublishedDate = new DateOnly(1862, 4, 3),
            Genre = "Roman historique",
            AuthorId = 1
        },
        new Book
        {
            Id = 2,
            Title = "Notre-Dame de Paris",
            ISBN = "9782070409280",
            Summary = "Le destin tragique de Quasimodo, sonneur de cloches bossu, et de la belle gitane Esméralda dans le Paris médiéval.",
            PageCount = 940,
            PublishedDate = new DateOnly(1831, 3, 16),
            Genre = "Roman historique",
            AuthorId = 1
        },
        new Book
        {
            Id = 3,
            Title = "L'Étranger",
            ISBN = "9782070360024",
            Summary = "Meursault, un homme indifférent au monde, commet un meurtre absurde et fait face à la justice avec une totale détachement.",
            PageCount = 186,
            PublishedDate = new DateOnly(1942, 6, 15),
            Genre = "Roman philosophique",
            AuthorId = 2
        },
        new Book
        {
            Id = 4,
            Title = "La Peste",
            ISBN = "9782070360123",
            Summary = "Une ville algérienne frappée par la peste bubonique ; chronique de la solidarité humaine face à l'absurde.",
            PageCount = 279,
            PublishedDate = new DateOnly(1947, 6, 10),
            Genre = "Roman philosophique",
            AuthorId = 2
        },
        new Book
        {
            Id = 5,
            Title = "Orgueil et Préjugés",
            ISBN = "9782070413119",
            Summary = "Elizabeth Bennet et le mystérieux Mr Darcy s'affrontent dans un ballet d'orgueil et de malentendus dans l'Angleterre georgienne.",
            PageCount = 432,
            PublishedDate = new DateOnly(1813, 1, 28),
            Genre = "Roman sentimental",
            AuthorId = 3
        },
        new Book
        {
            Id = 6,
            Title = "Raison et Sentiments",
            ISBN = "9782070413201",
            Summary = "Deux sœurs aux tempéraments opposés, Elinor et Marianne Dashwood, traversent épreuves et déceptions amoureuses.",
            PageCount = 374,
            PublishedDate = new DateOnly(1811, 10, 30),
            Genre = "Roman sentimental",
            AuthorId = 3
        }
    );

    // ── Members ───────────────────────────────────────────────────────────────
    modelBuilder.Entity<Member>().HasData(
        new Member
        {
            Id = 1,
            Email = "alice.martin@email.com",
            FirstName = "Alice",
            LastName = "Martin",
            MembershipDate = new DateOnly(2023, 1, 15),
            IsActive = true
        },
        new Member
        {
            Id = 2,
            Email = "bob.dupont@email.com",
            FirstName = "Bob",
            LastName = "Dupont",
            MembershipDate = new DateOnly(2023, 3, 22),
            IsActive = true
        },
        new Member
        {
            Id = 3,
            Email = "claire.leblanc@email.com",
            FirstName = "Claire",
            LastName = "Leblanc",
            MembershipDate = new DateOnly(2022, 11, 5),
            IsActive = true
        },
        new Member
        {
            Id = 4,
            Email = "david.bernard@email.com",
            FirstName = "David",
            LastName = "Bernard",
            MembershipDate = new DateOnly(2024, 2, 10),
            IsActive = false
        }
    );

    // ── Loans ─────────────────────────────────────────────────────────────────
    // 3 retournés + 2 en cours (ReturnDate = null)
    modelBuilder.Entity<Loan>().HasData(
        new Loan
        {
            Id = 1,
            LoanDate = new DateOnly(2024, 10, 1),
            DueDate = new DateOnly(2024, 10, 21),
            ReturnDate = new DateOnly(2024, 10, 18),   // rendu
            BookId = 1,
            MemberId = 1
        },
        new Loan
        {
            Id = 2,
            LoanDate = new DateOnly(2024, 11, 5),
            DueDate = new DateOnly(2024, 11, 25),
            ReturnDate = new DateOnly(2024, 11, 20),   // rendu
            BookId = 3,
            MemberId = 2
        },
        new Loan
        {
            Id = 3,
            LoanDate = new DateOnly(2024, 12, 10),
            DueDate = new DateOnly(2024, 12, 30),
            ReturnDate = new DateOnly(2024, 12, 28),   // rendu
            BookId = 5,
            MemberId = 3
        },
        new Loan
        {
            Id = 4,
            LoanDate = new DateOnly(2025, 3, 1),
            DueDate = new DateOnly(2025, 3, 21),
            ReturnDate = null,                         // EN COURS
            BookId = 2,
            MemberId = 1
        },
        new Loan
        {
            Id = 5,
            LoanDate = new DateOnly(2025, 3, 5),
            DueDate = new DateOnly(2025, 3, 25),
            ReturnDate = null,                         // EN COURS
            BookId = 4,
            MemberId = 3
        }
    );

    // ── Reviews ───────────────────────────────────────────────────────────────
    modelBuilder.Entity<Review>().HasData(
        new Review
        {
            Id = 1,
            Rating = 5,
            Comment = "Un chef-d'œuvre absolu, bouleversant de bout en bout.",
            CreatedAt = new DateTime(2024, 10, 20),
            BookId = 1,
            MemberId = 1
        },
        new Review
        {
            Id = 2,
            Rating = 4,
            Comment = "Très prenant, l'absurde prend tout son sens.",
            CreatedAt = new DateTime(2024, 11, 22),
            BookId = 3,
            MemberId = 2
        },
        new Review
        {
            Id = 3,
            Rating = 5,
            Comment = "Romantique et piquant, Austen est incomparable.",
            CreatedAt = new DateTime(2024, 12, 29),
            BookId = 5,
            MemberId = 3
        },
        new Review
        {
            Id = 4,
            Rating = 3,
            Comment = "Intéressant mais un peu lent par moments.",
            CreatedAt = new DateTime(2025, 1, 10),
            BookId = 6,
            MemberId = 4
        }
    );
    }
}
