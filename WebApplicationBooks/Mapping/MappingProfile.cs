using AutoMapper;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Mapping.Entity;

namespace WebApplicationBooks.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Book, DtoUrls>()
                .ForMember(dto => dto.UserUrl, s => s.MapFrom(book => book.Categoria + "/" + book.Url));
        }
    }
}