using bloodDonation.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            var donorData = new DonorDataDto();

            try
            {
                var validationData = await hash.ValidatePassword(username, password);
                donorData.Token = validationData.Item1;
                donorData.DonorID = validationData.Item2;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string jsonString = JsonSerializer.Serialize(donorData);
            return Ok(jsonString);
        }

        #region Classes
        public class DonorDataDto
        {
            public string Token { get; set; }
            public Guid DonorID { get; set; }
        }
        #endregion
    }
}
