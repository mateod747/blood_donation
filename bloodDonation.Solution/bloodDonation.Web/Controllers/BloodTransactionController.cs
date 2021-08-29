using bloodDonation.Common;
using bloodDonation.Factory;
using bloodDonation.Model;
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
        public readonly IBloodDonationFactory _bloodDonationFactory;
        public readonly IBloodTransactionFactory _bloodTransactionFactory;
        public readonly IDonorFactory _donorFactory;

        public BloodTransactionController(IBloodDonationFactory bloodDonationFactory, IBloodTransactionFactory bloodTransactionFactory, IDonorFactory donorFactory)
        {
            _bloodDonationFactory = bloodDonationFactory;
            _bloodTransactionFactory = bloodTransactionFactory;
            _donorFactory = donorFactory;
        }

        [HttpPost]
        public async Task<IActionResult> PostBloodTransaction([FromBody] BloodTransactionModelDto bloodTransactionModelDto, [FromHeader] string token)
        {
            var bloodDonation = new BloodDonationModel()
            {
                DateDonated = DateTime.Now,
                DonorID = Guid.Empty,
                BloodID = Guid.NewGuid()
            };

            var bloodTransaction = new BloodTransactionModel()
            {
                TransactID = Guid.NewGuid(),
                BloodID = bloodDonation.BloodID,
                BloodPressure = bloodTransactionModelDto.BloodPressure,
                DateOut = DateTime.Now,
                EmpID = bloodTransactionModelDto.EmpId,
                RecipientID = new Guid("AA4DE667-8D99-461A-BBA1-73815EBB0EFE"),
                Hemoglobin = bloodTransactionModelDto.Hemoglobin,
                Notes = bloodTransactionModelDto.Notes,
                Quantity = bloodTransactionModelDto.Quantity,
                Success = bloodTransactionModelDto.Success
            };

            var validationResult = JWTAuth.ValidateCurrentToken(token);

            var claim = String.Empty;

            if (!validationResult) return Ok(false);

            claim = JWTAuth.GetClaim(token, "UserRole");

            try
            {
                if (claim.Equals("Admin"))
                {
                    var donor = await _donorFactory.GetDonorByUsername(bloodTransactionModelDto.Username);

                    if (donor == null || donor.DonorID == Guid.Empty) return Ok(false);

                    bloodDonation.DonorID = donor.DonorID;

                    var result = await _bloodDonationFactory.PostBloodDonation(bloodDonation);

                    if (!result) return Ok(false);

                    var resultTransaction = await _bloodTransactionFactory.PostBloodTransaction(bloodTransaction);

                    if (!result) 
                    {
                        _ = await _bloodDonationFactory.DeleteBloodDonation(bloodDonation.BloodID);
                        return Ok(false);
                    };

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> EditBloodTransaction([FromBody] BloodTransactionModelDto bloodTransactionModelDto, [FromHeader] string token)
        {
            var bloodTransaction = new BloodTransactionModel()
            {
                TransactID = bloodTransactionModelDto.TransactId,
                RecipientID = bloodTransactionModelDto.RecipientId,
                Notes = bloodTransactionModelDto.Notes
            };

            var validationResult = JWTAuth.ValidateCurrentToken(token);

            var claim = String.Empty;

            if (!validationResult) return Ok(false);

            claim = JWTAuth.GetClaim(token, "UserRole");

            try
            {
                if (claim.Equals("Admin"))
                {                    
                    var result = await _bloodTransactionFactory.EditBloodTransaction(bloodTransaction);

                    if (result == false) return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetBloodTransaction(int day, int month, int year, [FromBody] BloodTransactionModelDto bloodTransactionModelDto, [FromHeader] string token)
        {
            var validationResult = JWTAuth.ValidateCurrentToken(token);

            var claim = String.Empty;

            if (!validationResult) return Ok(false);

            claim = JWTAuth.GetClaim(token, "UserRole");

            var transaction = new BloodTransactionModel()
            {
                TransactID = Guid.Empty,
            };

            try
            {
                if (claim.Equals("Admin"))
                {
                    var donor = await _donorFactory.GetDonorByUsername(bloodTransactionModelDto.Username);

                    if(donor == null || donor.DonorID == Guid.Empty)
                    {
                        return Ok(transaction);
                    }

                    var bloodDonation = await _bloodDonationFactory.GetBloodDonationByDateIn(
                                                year,
                                                month,
                                                day,
                                                donor.DonorID);

                    if (bloodDonation == null || bloodDonation.BloodID == Guid.Empty)
                    {
                        return Ok(transaction);
                    }

                    var result = await _bloodTransactionFactory.GetBloodTransaction(bloodDonation.BloodID);

                    if (result == null || result.TransactID == Guid.Empty)
                    {
                        return Ok(transaction);
                    }

                    transaction.TransactID = result.TransactID;
                    transaction.Notes = result.Notes;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(transaction);
        }

        #region Classes 

        public class TransactionDataDto
        {
            public string Username { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
        }

        public class BloodTransactionModelDto
        {
            public string Username { get; set; }
            public Guid DonorId { get; set; }
            public Guid TransactId { get; set; }
            public Guid BloodId { get; set; }
            public Guid RecipientId { get; set; }
            public Guid EmpId { get; set; }
            public int DateInYear { get; set; }
            public int DateInMonth { get; set; }
            public int DateInDay { get; set; }
            public int DateOutYear { get; set; }
            public int DateOutMonth { get; set; }
            public int DateOutDay { get; set; }
            public int Quantity { get; set; }
            public int Hemoglobin { get; set; }
            public string BloodPressure { get; set; }
            public string Notes { get; set; }
            public bool Success { get; set; }
        }



        #endregion
    }
}
