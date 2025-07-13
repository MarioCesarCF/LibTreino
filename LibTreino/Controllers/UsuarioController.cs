using LibTreino.Models;
using LibTreino.Models.ViewModels.Usuario;
using LibTreino.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibTreino.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _userService;
        private readonly TokenService _tokenService;

        public UsuarioController(UsuarioService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Usuario>> GetUsersAsync()
        {
            return await _userService.GetAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<Usuario> RetornaUserAsync(string id)
        {
            return await _userService.GetAsync(id);
        }

        [HttpPost]
        public async Task<Usuario> CreateUserAsync(CreateUser newUser)
        {
            var user = await _userService.CreateAsync(newUser);

            return user;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task UpdateUserAsync(string id, UpdateUser updateUser)
        {
            await _userService.UpdateAsync(id, updateUser);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task RemoveUserAsync(string id)
        {
            await _userService.RemoveAsync(id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            var user = await _userService.GetByEmailAsync(loginUser.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
                return Unauthorized("Usuário ou senha inválidos");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
