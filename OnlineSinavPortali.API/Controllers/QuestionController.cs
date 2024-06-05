using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSinavPortali.API.Dtos;
using OnlineSinavPortali.API.Models;

namespace OnlineSinavPortali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public QuestionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<QuestionDto> GetList()
        {
            var questions = _context.Questions.ToList();
            var questionsDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionsDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public QuestionDto Get(int id)
        {
            var question = _context.Questions.Where(s => s.Id == id).SingleOrDefault();
            var questionDto = _mapper.Map<QuestionDto>(question);
            return questionDto;
        }
        [HttpGet]
        [Route("{id}/Answers")]
        public List<AnswerDto> GetAnswers(int id)
        {
            var Answer = _context.Answers.Where(s => s.QuestionId == id).ToList();
            var AnswerDto = _mapper.Map<List<AnswerDto>>(Answer);
            return AnswerDto;
        }

        [HttpPost]
        public ResultDto Post(QuestionDto dto)
        {
            if (_context.Questions.Count(c => c.Text == dto.Text) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Soru Kayıtlıdır!";
                return result;
            }
            var question = _mapper.Map<Question>(dto);

            _context.Questions.Add(question);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Soru Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(QuestionDto dto)
        {
            var question = _context.Questions.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (question == null)
            {
                result.Status = false;
                result.Message = "Soru Bulunamadı!";
                return result;
            }
            question.Text = dto.Text;



            question.ExamId = dto.ExamId;
            _context.Questions.Update(question);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Soru Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var Question = _context.Questions.Where(s => s.Id == id).SingleOrDefault();
            if (Question == null)
            {
                result.Status = false;
                result.Message = "Soru Bulunamadı!";
                return result;
            }
            _context.Questions.Remove(Question);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Soru Silindi";
            return result;
        }
    }
}