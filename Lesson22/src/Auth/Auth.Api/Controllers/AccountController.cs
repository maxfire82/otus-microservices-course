using System.Security.Claims;
using Auth.Api.Models;
using Auth.Domain.Services;
using Common.Authentication.Services;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Auth.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        /// <inheritdoc />
        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Создание аккаунта.
        /// </summary>
        /// <param name="account">Данные аккаунта</param>
        /// <returns>Аккаунт создан</returns>
        /// <response code="200">Аккаунт создан</response>
        /// <response code="400">Некорректные данные.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AccountCreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountCreateResponse>> CreateAsync([FromBody] AccountCreateRequest account)
        {
            if (!ModelState.IsValid || account == null)
            {
                return BadRequest("Некорректные данные");
            }    
            
            var createdAccount = await _accountService.CreateAsync(account.Login, account.Password);
            return Ok(new AccountCreateResponse
            {
                Id = createdAccount.Id,
                Login = createdAccount.Login
            });
        }
        
        /// <summary>
        ///     Создает токен.
        /// </summary>
        /// <param name="request">Данные аккаунта</param>
        /// <returns>Аккаунт создан</returns>
        /// <response code="200">Аккаунт создан</response>
        /// <response code="400">Некорректные данные.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTokenResponse>> CreateTokenAsync([FromBody] CreateTokenRequest request)
        {
            if (request == default)
            {
                return BadRequest("Некорректные данные");
            }
            
            var accountDb = await _accountService.LoginAsync(request.Login, request.Password);

            if (accountDb == null)
            {
                return NotFound("Account not found");
            }

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountDb.Login),
                new Claim("account_id", accountDb.Id.ToString()),
                new Claim("scope", $"GENERAL"),
            };
            var token = _tokenService.CreateToken(claims);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            
            return Ok(new CreateTokenResponse
            {
                Token = token
            });
        }
       
    }
}
