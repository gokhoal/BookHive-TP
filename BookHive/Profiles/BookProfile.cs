using AutoMapper;
using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using BookHive.Models;

namespace BookHive.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, GetBookDto>().ForMember(
            dest => dest.AuthorFullName,
            opt => opt.MapFrom(src => $"{src.Author!.FirstName} {src.Author.LastName}")
        );;;
        CreateMap<Book, GetBookDetailsDto>().ForMember(
                dest => dest.AverageRating,
                opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0)
            )
            .ForMember(
                dest => dest.ReviewCount,
                opt => opt.MapFrom(src => src.Reviews.Count)
            );
            
        CreateMap<CreateBookRequestDto, Book>();
        CreateMap<UpdateBookRequestDto, Book>();
    }
}