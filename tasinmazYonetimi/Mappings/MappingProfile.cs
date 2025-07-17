using AutoMapper;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Dtos;

namespace tasinmazYonetimi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Il, IlDto>().ReverseMap();
            CreateMap<Ilce, IlceDto>().ReverseMap();
            CreateMap<Mahalle, MahalleDto>().ReverseMap();
            CreateMap<Kullanici, KullaniciDto>().ReverseMap();
            CreateMap<Log, LogDto>().ReverseMap();
            CreateMap<Tasinmaz, TasinmazDto>().ReverseMap();
        }
    }
}