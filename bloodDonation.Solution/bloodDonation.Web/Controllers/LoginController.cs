using bloodDonation.Factory;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace bloodDonation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginFactory _loginFactory;

        public LoginController(ILoginFactory loginFactory)
        {
            _loginFactory = loginFactory;
        }

        [EnableCors]
        [HttpGet]
        public async Task<IActionResult> ValidateLogin(string username, string password)
        {
            var donorData = new DonorDataDto();

            try
            {
                var validationData = await _loginFactory.GetLoginData(username, password);
                donorData.Token = validationData.Item1;
                donorData.DonorID = validationData.Item2;
                donorData.Admin = validationData.Item3;
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
            public bool Admin { get; set; }
        }
        #endregion
    }
}
