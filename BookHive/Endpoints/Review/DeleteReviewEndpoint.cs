using BookHive;
using BookHive.DTO.Review.Request;
using BookHive.DTO.Review.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class DeleteReviewEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetReviewDto, GetReviewDetailsDto>
{
    public override void Configure()
    {
        
        Post("/reviews/{@id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetReviewDto req, CancellationToken ct)
    {
        BookHive.Models.Review? databaseReview =
            await bookHiveDbContext.Reviews
                .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseReview == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Reviews.Remove(databaseReview);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}