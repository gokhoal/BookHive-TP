using BookHive;
using BookHive.DTO.Review.Request;
using BookHive.DTO.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class GetReviewEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetReviewDto, GetReviewDetailsDto>
{
    public override void Configure()
    { 
        AllowAnonymous();
        Get("/books/{@BookId}/reviews", x => new { x.Id });
    }

    public override async Task HandleAsync(GetReviewDto req, CancellationToken ct)
    {
        BookHive.Models.Review? databaseReview = await bookHiveDbContext.Reviews
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        
        if (databaseReview == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        GetReviewDetailsDto dto = new()
        {
            Id = databaseReview.Id,
            Rating = databaseReview.Rating,
            Comment = databaseReview.Comment,
            CreatedAt = databaseReview.CreatedAt,
            BookId = databaseReview.BookId,
            MemberId = databaseReview.MemberId
        };
        
        await Send.OkAsync(dto, ct);
    }
}