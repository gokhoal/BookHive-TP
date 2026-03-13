using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Author;

public class GetAllAuthorsEndpoint(BookHiveDbContext bookHiveDbContext) : EndpointWithoutRequest<List<GetAuthorDetailsDto>>
{
    public override void Configure()
    {
        Get ("/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetAuthorDetailsDto> authors= await bookHiveDbContext.Authors.Select(x => new GetAuthorDetailsDto()
            {
                Id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Biography = x.Biography,
                BirthDate = x.BirthDate,
                Nationality = x.Nationality
            
            })
            .ToListAsync(ct);
        
        await Send.OkAsync(authors, ct);
    }
}