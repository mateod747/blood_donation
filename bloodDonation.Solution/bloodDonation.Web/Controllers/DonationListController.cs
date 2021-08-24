using bloodDonation.Common;
using bloodDonation.Factory;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static bloodDonation.Factory.DonationListFactory;

namespace bloodDonation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationListController : ControllerBase
    {
        private readonly IDonationListFactory _donationListFactory;

        public DonationListController(IDonationListFactory donationListFactory)
        {
            _donationListFactory = donationListFactory;
        }

        [EnableCors]
        [HttpGet]
        public async Task<IActionResult> GetDonationList(int page, int pageSize, Guid id, [FromHeader] string token)
        {
            try
            {
                var validationResult = JWTAuth.ValidateCurrentToken(token);

                var claim = String.Empty;
                var modelDto = new DonationListDto() 
                {
                    Donations = new List<DonationDto>()    
                };

                if (!validationResult) return Ok(modelDto);

                claim = JWTAuth.GetClaim(token, "UserRole");

                if (claim.Equals("User"))
                {
                    modelDto = await _donationListFactory.GetDonationListAsync(page, pageSize, id);
                }

                return Ok(modelDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
