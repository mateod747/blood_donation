using bloodDonation.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bloodDonation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [EnableCors]
        [HttpGet]
        public async Task<IActionResult> ValidateLogin(string username, string password)
        {
            PasswordHash hash = new PasswordHash();
            var token = String.Empty;

            try
            {
                token = await hash.ValidatePassword(username, password);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(token);
        }
    }
}
