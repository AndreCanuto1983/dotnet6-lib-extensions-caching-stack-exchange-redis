using AnotherWayToImplementRedis.Interfaces;
using AnotherWayToImplementRedis.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Set(UserModel user, CancellationToken cancellationToken)
        {
            if (user.IsValid())
                return BadRequest();

            await _userRepository.SetUserAsync(user, cancellationToken);

            return Created("","");
        }

        [HttpGet("{userId}")]        
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var response = await _userRepository.GetUserAsync(userId, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UserModel user, CancellationToken cancellationToken)
        {
            if (user.IsValid())
                return BadRequest();

            await _userRepository.UpdateUserAsync(user, cancellationToken);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            await _userRepository.DeleteUserAsync(userId, cancellationToken);

            return Ok();
        }
    }
}