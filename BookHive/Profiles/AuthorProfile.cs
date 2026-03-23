using AutoMapper;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using BookHive.Models;

namespace BookHive.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, GetAuthorDto>();
        CreateMap<Author, GetAuthorDetailsDto>()
            .ForMember(
                dest => dest.Books,
                opt => opt.MapFrom(src => src.Books)
            );
        CreateMap<CreateAuthorRequestDto, Author>();
        CreateMap<UpdateAuthorRequestDto, Author>();
    }
}