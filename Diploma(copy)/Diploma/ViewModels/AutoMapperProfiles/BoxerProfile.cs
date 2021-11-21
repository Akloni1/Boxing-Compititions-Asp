using AutoMapper;
using Diploma.ViewModels.Boxers;


namespace Diploma.ViewModels.AutoMapperProfiles


{
    public class BoxerProfile : Profile
    {
        public BoxerProfile()
        {
            CreateMap<Diploma.Boxers, InputBoxerViewModel>().ReverseMap();
            CreateMap<Diploma.Boxers, DeleteBoxerViewModel>();
            CreateMap<Diploma.Boxers, EditBoxerViewModel>().ReverseMap();
            CreateMap<Diploma.Boxers, BoxerViewModel>();
           
        }
    }
}