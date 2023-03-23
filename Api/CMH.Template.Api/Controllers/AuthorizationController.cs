using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CMH.VMF.Security.JwtCommon.Interfaces;

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
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IJwtValidator _jwtValidator;

        public AuthorizationController(IJwtGenerator jwtGenerator, IJwtValidator jwtValidator)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _jwtValidator = jwtValidator ?? throw new ArgumentNullException(nameof(jwtValidator));
        }

        /// <summary>Validate a token</summary>
        /// <param name="jwt">the jwt token to be validated</param>
        /// <returns>bool: True if valid token, else false</returns>
        /// <response code="200">The token was validated successfully</response>
        [HttpPost("validate")]
        [Produces("application/json")]
        public async Task<IActionResult> ValidateTokenAsync([FromBody] CMH.VMF.Security.JwtCommon.Models.JWT jwt) 
        {
            var ret = await Task.FromResult(_jwtValidator.IsValid(jwt));

            return Ok(ret);
        }

        /// <summary>Generate a token</summary>
        /// <param name="customClaims"> Allows the generation of a new token with custom settings if not null </param>
        /// <returns>A token for this domain</returns>
        /// <response code="200">The Token was generated successfully</response>
        [HttpPost("generate")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CMH.VMF.Security.JwtCommon.Models.JWT), 200)]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] IEnumerable<System.Security.Claims.Claim> customClaims = null)
        {
            var ret = customClaims != null ? await Task.FromResult(_jwtGenerator.GenerateToken(customClaims)) : await Task.FromResult(_jwtGenerator.GenerateToken());

            return Ok(ret);
        }

        /// <summary>
        /// This method will strip off all the default claims on the token passed in and add the custom claims to the new token.
        /// </summary>
        /// <param name="jwt">Existing Token to be swapped out for an internal token.</param>
        /// <returns>A token for this domain</returns>
        /// <response code="200">The Token was swapped successfully</response>
        [HttpPost("swap")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CMH.VMF.Security.JwtCommon.Models.JWT), 200)]
        public async Task<IActionResult> SwapTokenAsync([FromBody] CMH.VMF.Security.JwtCommon.Models.JWT jwt)
        {
            var ret = await Task.FromResult(_jwtGenerator.SwapToken(jwt));

            return Ok(ret);
        }
    }
}
