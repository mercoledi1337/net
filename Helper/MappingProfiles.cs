using AutoMapper;
using Przychodnia.Dto;
using Przychodnia.Models;

namespace Przychodnia.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Uzytkownik, UzytkownikDto>();
            CreateMap<UzytkownikDto, Uzytkownik>();
            CreateMap<Lekarz, LekarzDto>();
            CreateMap<LekarzDto, Lekarz>();
            CreateMap<Pacjent, PacjentDto>();
            CreateMap<PacjentDto, Pacjent>();
            CreateMap<WizytaDto, Wizyta>();
            CreateMap<Wizyta, WizytaDto>();
        }
    }
}
