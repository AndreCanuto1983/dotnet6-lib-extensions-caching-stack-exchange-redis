using AnotherWayToImplementRedis.Interfaces;
using AnotherWayToImplementRedis.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AnotherWayToImplementRedis.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnotherWayToImplementRedisController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AnotherWayToImplementRedisController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Set(UserModel user, CancellationToken cancellationToken)
        {
            await _userRepository.SetUserAsync(user, cancellationToken);
            return Created("","");
        }

        [HttpGet("{cpfCnpj}")]        
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string cpfCnpj, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(cpfCnpj))
                return BadRequest();

            var response = await _userRepository.GetUserAsync(cpfCnpj, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UserModel user, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateUserAsync(user, cancellationToken);
            return Ok();
        }

        [HttpDelete("{cpfCnpj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string cpfCnpj, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(cpfCnpj, @"^[0-9]+$"))
                return BadRequest("Please enter numbers only in Cpf/Cnpj");

            if (cpfCnpj.Length > 14)
                return BadRequest("Cpf/Cnpj must not be longer than 14 characters");

            await _userRepository.DeleteUserAsync(cpfCnpj, cancellationToken);
            return Ok();
        }
    }
}