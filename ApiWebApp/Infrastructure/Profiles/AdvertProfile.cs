using ApiWebApp.Data;
using ApiWebApp.DTO;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace ApiWebApp.Infrastructure.Profiles
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<Advert, EditDTO>()
                .ReverseMap();

            CreateMap<Advert, CreateDTO>()
                .ReverseMap();

            CreateMap<Advert, AdvertDTO>()
                .ReverseMap();
            CreateMap<JsonPatchDocument<AdvertDTO>, JsonPatchDocument<Advert>>()
                .ReverseMap();
            CreateMap<Operation<AdvertDTO>, Operation<Advert>>();
            CreateMap<Advert, List<AdvertDTO>>()
                .ReverseMap();
        }
    }
}
