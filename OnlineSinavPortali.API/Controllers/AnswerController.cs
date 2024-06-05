using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSinavPortali.API.Dtos;
using OnlineSinavPortali.API.Models;

namespace OnlineSinavPortali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnswerDto>> GetAnswers()
        {
            var answerDtos = _context.Answers
                .Select(a => new AnswerDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    TextAnswer = a.TextAnswer,
                    QuestionId = a.QuestionId
                })
                .ToList();

            return Ok(answerDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<AnswerDto> GetAnswer(int id)
        {
            var answer = _context.Answers.Find(id);

            if (answer == null)
            {
                return NotFound();
            }

            var answerDto = new AnswerDto
            {
                Id = answer.Id,
                UserId = answer.UserId,
                TextAnswer = answer.TextAnswer,
                QuestionId = answer.QuestionId
            };

            return Ok(answerDto);
        }

        [HttpPost]
        public ActionResult<AnswerDto> PostAnswer(AnswerDto answerDto)
        {
            var answer = new Answer
            {
                UserId = answerDto.UserId,
                TextAnswer = answerDto.TextAnswer,
                QuestionId = answerDto.QuestionId
            };

            _context.Answers.Add(answer);
            _context.SaveChanges();

            answerDto.Id = answer.Id;

            return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, answerDto);
        }

        [HttpPut("{id}")]
        public IActionResult PutAnswer(int id, AnswerDto answerDto)
        {
            if (id != answerDto.Id)
            {
                return BadRequest();
            }

            var answer = _context.Answers.Find(id);

            if (answer == null)
            {
                return NotFound();
            }

            answer.UserId = answerDto.UserId;
            answer.TextAnswer = answerDto.TextAnswer;
            answer.QuestionId = answerDto.QuestionId;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnswer(int id)
        {
            var answer = _context.Answers.Find(id);

            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}