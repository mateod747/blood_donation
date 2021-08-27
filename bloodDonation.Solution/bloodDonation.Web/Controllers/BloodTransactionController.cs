using bloodDonation.Common;
using bloodDonation.Factory;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bloodDonation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodTransactionController : ControllerBase
    {
        public readonly IBloodTransactionFactory _bloodTransactionFactory;

        public BloodTransactionController(IBloodTransactionFactory bloodTransactionFactory)
        {
            _bloodTransactionFactory = bloodTransactionFactory;
        }

        [HttpPost]
        public async Task<IActionResult> GetDonorIdAndCheckIfPersonnelExists([FromBody]DonorDataDto donorDataDto, [FromHeader] string token)
        {
            var data = new DonorCheckDto()
            {
                DonorId = Guid.Empty,
                Message = "Not validated"
            };
            var validationResult = JWTAuth.ValidateCurrentToken(token);

            var claim = String.Empty;

            if (!validationResult) return Ok(false);

            claim = JWTAuth.GetClaim(token, "UserRole");

            try
            {
                if(claim.Equals("Admin"))
                {
                    var result = await _bloodTransactionFactory.GetDonorIdAndCheckIfPersonnelExists(donorDataDto.Username, donorDataDto.PersonnelId);
                    data = new DonorCheckDto()
                    {
                        DonorId = result.Item1,
                        Message = result.Item2
                    };
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(data);
        }

        #region Classes 

        public class DonorDataDto
        {
            public Guid PersonnelId { get; set; }
            public string Username { get; set; }
        }

        public class DonorCheckDto
        {
            public Guid DonorId { get; set; }
            public string Message { get; set; }
        }


        #endregion
    }
}
