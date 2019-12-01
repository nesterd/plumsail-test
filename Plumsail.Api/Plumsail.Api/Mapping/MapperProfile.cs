using AutoMapper;
using Plumsail.Api.Models.Options;
using Plumsail.Api.Models.Somethings;
using Plumsail.Api.Utils;
using Plumsail.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plumsail.Api.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Something, SomethingViewModel>()
                .ForMember(d => d.Time, o => o.MapFrom(s => s.Time.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.GetDescription()));
            CreateMap<SomethingType, IntOption>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Text, o => o.MapFrom(s => s.Name));
            CreateMap<SomethingCreateViewModel, Something>()
                .ForMember(d => d.Time, o => o.MapFrom(s => DateTime.Parse(s.Time)));
            CreateMap<SomethingEditViewModel, Something>()
                .ForMember(d => d.Time, o => o.MapFrom(s => DateTime.Parse(s.Time)));
        }
    }
}
