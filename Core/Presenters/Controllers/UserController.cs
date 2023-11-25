using Core.Domain.Entities;
using Core.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository=userRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            userRepository.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            var existingUser = userRepository.Get(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            user.UserId = id;
            userRepository.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = userRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userRepository.GetAll();
            return Ok(users);
        }
    }
}