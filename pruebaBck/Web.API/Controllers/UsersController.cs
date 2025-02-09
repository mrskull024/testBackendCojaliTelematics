using Application.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, IEmailService emailService) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEmailService _emailService = emailService;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserAddDTO user)
        {
            try
            {
                bool exists = await _userRepository.GetUserByEmail(user.Email);

                if (exists)
                {
                    return BadRequest($"El usuario con el email {user.Email} ya se encuentra registrado");
                }

                int result = await _userRepository.AddUser(user);

                if (result == 1)
                {
                    await _emailService.SendEmail(user.Email, "Bienvenido", "Gracias por registrarte en nuestra plataforma");
                    return Created();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
