using AutoMapper;
using System;
using WebApplication_AutoMapper.Model;

namespace WebApplication_AutoMapper.Mapper
{
    /// <summary>
    /// 参考 https://docs.automapper.org/en/stable/Getting-started.html
    /// https://github.com/AutoMapper/AutoMapper
    /// </summary>
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorModel, AuthorDTO>()
               //字段名称不一致 Name映射到OrderName
               .ForMember(destination => destination.Address, map => map.MapFrom(source => source.Address1))
               //null时,设置默认值
               .ForMember(destination => destination.LastName, opt => opt.NullSubstitute("No data"))
                //字段类型不一致 DateTime映射到String
                .ForMember(dest => dest.CreateTime, src => src.ConvertUsing(new FormatConvert()))
                .ReverseMap();
        }
    }

    /// <summary>
    /// DateTime映射到String
    /// </summary>
    public class FormatConvert : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime sourceMember, ResolutionContext context)
        {
            if (sourceMember == null)
                return "";
            return sourceMember.ToString("yyyyMMddHHmmssfff");
        }
    }
}
