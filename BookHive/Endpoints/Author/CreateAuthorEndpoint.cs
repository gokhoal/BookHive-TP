using BookHive;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using FluentValidation;

namespace Bookhive.Endpoints.Author;

public class CreateAuthorEndpoint(BookHiveDbContext db, AutoMapper.IMapper mapper) : Endpoint<CreateAuthorRequestDto>
{
    public override async Task HandleAsync(CreateAuthorRequestDto req, CancellationToken ct)
    {
        BookHive.Models.Author? author = mapper.Map<BookHive.Models.Author>(req);
        db.Authors.Add(author);
        await db.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}