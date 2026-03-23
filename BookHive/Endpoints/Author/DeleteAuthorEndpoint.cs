using BookHive;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Bookhive.Endpoints.Author;

public class DeleteAuthorEndpoint(BookHiveDbContext db, AutoMapper.IMapper mapper) : Endpoint<GetAuthorDto, GetAuthorDetailsDto>
{

    public override async Task HandleAsync(GetAuthorDto req, CancellationToken ct)
    {
        BookHive.Models.Author? author = mapper.Map<BookHive.Models.Author?>(req);
        db.Authors.Remove(author);
        await db.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}