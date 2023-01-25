using AutoMapper;
using MvcSocialWeb.Data.DBModel;
using MvcSocialWeb.ViewModels.Account;

namespace MvcSocialWeb
{
    /// <summary>
    /// Настройка маппинга всех сущностей приложения
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(user => user.Email, opt => opt.MapFrom(model => model.EmailReg))
                .ForMember(user => user.UserName, opt => opt.MapFrom(model => model.Login))
                .ForMember(user => user.BirthDate, opt => opt.MapFrom(model => new DateTime((int)model.Year, (int)model.Month, (int)model.Date)))
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(model => model.PasswordReg.GetHashCode()));
        }
    }
}
