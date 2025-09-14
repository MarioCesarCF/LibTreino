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

        public UsuarioController(UsuarioService userService)
        {
            _userService = userService;
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
    }
}
