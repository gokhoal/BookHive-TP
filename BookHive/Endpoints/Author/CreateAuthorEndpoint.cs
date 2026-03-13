using BookHive;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;

namespace Bookhive.Endpoints.Author;

public class CreateAuthorEndpoint(BookHiveDbContext bookhiveDbContext) : Endpoint<CreateAuthorRequestDto, GetAuthorDetailsDto>
{
    public override void Configure()
    {
        Post("/authors");
        AllowAnonymous();
    }


    public override async Task HandleAsync(CreateAuthorRequestDto req, CancellationToken ct)
    {
         BookHive.Models.Author author = new()
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Biography = req.Biography,
            BirthDate = req.BirthDate,
            Nationality = req.Nationality
            
        };
        bookhiveDbContext.Add(author);
        await bookhiveDbContext.SaveChangesAsync(ct);

        GetAuthorDetailsDto details = new()
        {
            Id = author.Id,
            LastName = author.LastName,
            FirstName = author.FirstName,
            Biography =  author.Biography,
            BirthDate = author.BirthDate,
            Nationality = author.Nationality
            
        };

        await Send.OkAsync(details, ct);

    }
}