using AutoMapper;

using WebTestApi.Shared.Models;

namespace WebTestApi.Helper
{
    public class AppMapper : Profile
    {
       

        public AppMapper()
        {
            CreateMap<Student, StudentDto>();
        }
    }
}
