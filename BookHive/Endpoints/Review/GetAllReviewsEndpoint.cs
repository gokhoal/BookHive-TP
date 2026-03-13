using BookHive;
using BookHive.DTO.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class GetAllReviewsEndpoint(BookHiveDbContext bookhiveDbContext) : EndpointWithoutRequest<List<GetReviewDetailsDto>>
{
    public override void Configure()
    {
        Get("/reviews");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetReviewDetailsDto> reviews = await bookhiveDbContext.Reviews
            .Select(r => new GetReviewDetailsDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                BookId = r.BookId,
                MemberId = r.MemberId
            })
            .ToListAsync(ct);

        await Send.OkAsync(reviews, ct);
    }
}