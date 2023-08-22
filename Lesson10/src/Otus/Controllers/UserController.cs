using Microsoft.AspNetCore.Mvc;
using Otus.Users.Models;
using Otus.Users.Services;

namespace Otus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <inheritdoc />
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Создание пользователя.
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        /// <returns>Пользователь создан</returns>
        /// <response code="200">Пользователь создан</response>
        /// <response code="400">Некорректные данные.</response>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> CreateAsync([FromBody] User user)
        {
            if (user == default)
            {
                return BadRequest("Некорректные данные");
            }
            
            var createdUser = await _userService.CreateAsync(user);
            return Ok(createdUser);
        }
        
        /// <summary>
        ///     Получение пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Задача</returns>
        /// <response code="200">Пользователь найден.</response>
        /// <response code="404">Пользователь не найден.</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetAsync(long userId)
        {
            try
            {
                var user = await _userService.GetAsync(userId);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        ///     Редактирование пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="user">Данные пользователя</param>
        /// <response code="200">Пользователь обновлен</response>
        /// <response code="400">Некорректные данные.</response>
        /// <response code="404">Пользователь не найден.</response>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(long userId, [FromBody] User user)
        {
            if (user == default || userId <= 0)
            {
                return BadRequest("Некорректные данные");
            }

            try
            {
                user.Id = userId;
                await _userService.UpdateAsync(user);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
        
        /// <summary>
        ///     Удаление пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <response code="200">Пользователь удален.</response>
        /// <response code="400">Некорректные данные.</response>
        /// <response code="404">Пользователь не найден.</response>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveAsync(long userId)
        {
            try
            {
                await _userService.RemoveAsync(userId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
