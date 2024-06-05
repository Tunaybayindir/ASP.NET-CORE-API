using AutoMapper;
using OnlineSinavPortali.API.Dtos;
using OnlineSinavPortali.API.Models;

namespace OnlineSinavPortali.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();

        }
    }
}