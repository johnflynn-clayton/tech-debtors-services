using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CMH.VMF.Security.JwtCommon.Interfaces;
using Microsoft.Extensions.Options;

namespace CMH.MobileHomeTracker.Api.Controllers
{
    /// <summary>
    /// With auth turned on we need a way of obtaining a valid token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        private readonly IOptions<AuthorizationSettings> _authorizationSettings;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthorizationController(IOptions<AuthorizationSettings> authorizationSettings, IJwtGenerator jwtGenerator)
        {
            _authorizationSettings = authorizationSettings ?? throw new ArgumentNullException(nameof(authorizationSettings));
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
        }

        /// <summary>Generate a token</summary>
        /// <param name="secret">The secret that will be validated and allow a token to be generated</param>
        /// <remarks>
        /// ### Validation
        /// secret
        /// * Must match the secret configured for this domain and environment
        /// 
        /// </remarks>
        /// <returns>A token for this domain</returns>
        /// <response code="200">The Token was generated successfully</response>
        /// <response code="401">The secret does not match the configured value</response>
        [HttpGet("token/{secret}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CMH.VMF.Security.JwtCommon.Models.JWT), 200)]
        public async Task<IActionResult> GetTokenAsync(string secret)
        {
            if(secret != _authorizationSettings.Value.Secret)
            {
                return Unauthorized();
            }

            var ret = await Task.FromResult(_jwtGenerator.JWT);

            return Ok(ret);
        }
    }
}
