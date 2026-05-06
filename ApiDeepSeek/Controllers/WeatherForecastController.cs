using ApiDeepSeek.Aplacation.InterfaceServies;
using ApiDeepSeek.models;
using ApiDeepSeek.Models;
using GroupApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IFactService _factService;
        private IJWTokenServiescs _jWTokenServiescs;
        public AnswersController(AppDbContext context,IFactService factService, IJWTokenServiescs jWTokenServiescs  )
        {
            _context = context;
            _factService = factService;
            _jWTokenServiescs = jWTokenServiescs;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
           var check  = await _jWTokenServiescs.SignAnonimal(user);
            return Ok(check);
        }
        [Authorize]
        [HttpPost("save-otvet")]
        public async Task<IActionResult> Create([FromBody] Answer answer)
        {
            var userId = User.FindFirst("userId")?.Value;
            answer.UserId = userId;
            await _factService.FactSave(answer);
            return Ok(answer);
        }
        [Authorize]
        [HttpGet("facts-user")]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst("userId")?.Value;
            var answers = await _factService.GetAnswers(userId);
            return Ok(answers);
        }
    }
}