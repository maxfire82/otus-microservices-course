using Common.Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Models;
using Users.Domain.Services;

namespace Users.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        /// <inheritdoc />
        public UserController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
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
            if (!ModelState.IsValid || user == null)
            {
                return BadRequest("Некорректные данные");
            }
            
            user.AccountId = _identityService.GetAccountId();;
            var createdUser = await _userService.CreateAsync(user);
            return Ok(createdUser);
        }
        
        /// <summary>
        ///     Получение пользователя.
        /// </summary>
        /// <param name="accountId">Идентификатор аккаунта</param>
        /// <returns>Задача</returns>
        /// <response code="200">Пользователь найден.</response>
        /// <response code="404">Пользователь не найден.</response>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetAsync(long accountId)
        {
            var id = _identityService.GetAccountId();
            if (accountId != id)
            {
                return Forbid();
            }
            try
            {
                var user = await _userService.GetAsync(accountId);
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
        /// <param name="accountId">Идентификатор пользователя.</param>
        /// <param name="user">Данные пользователя</param>
        /// <response code="200">Пользователь обновлен</response>
        /// <response code="400">Некорректные данные.</response>
        /// <response code="404">Пользователь не найден.</response>
        [HttpPut("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(long accountId, [FromBody] User user)
        {
            if (!ModelState.IsValid || user == null || accountId <= 0)
            {
                return BadRequest("Некорректные данные");
            }
            
            var id = _identityService.GetAccountId();
            if (accountId != id)
            {
                return Forbid();
            }

            try
            {
                user.AccountId = accountId;
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
        /// <param name="accountId">Идентификатор пользователя.</param>
        /// <response code="200">Пользователь удален.</response>
        /// <response code="400">Некорректные данные.</response>
        /// <response code="404">Пользователь не найден.</response>
        [HttpDelete("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveAsync(long accountId)
        {
            var id = _identityService.GetAccountId();
            if (accountId != id)
            {
                return Forbid();
            }
            
            try
            {
                await _userService.RemoveAsync(accountId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
