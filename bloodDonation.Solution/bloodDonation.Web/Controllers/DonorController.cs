using bloodDonation.Common;
using bloodDonation.Factory;
using bloodDonation.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace bloodDonation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorFactory _donorFactory;
        public DonorController(IDonorFactory donorFactory)
        {
            _donorFactory = donorFactory;
        }

        [EnableCors]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonor(Guid id, [FromHeader] string token)
        {
            try
            {
                var validationResult = JWTAuth.ValidateCurrentToken(token);

                var claim = String.Empty;
                var modelDto = new DonorModelDto();

                if (!validationResult) return Ok(modelDto);

                claim = JWTAuth.GetClaim(token, "UserRole");

                if(claim.Equals("User"))
                {
                    var model = await _donorFactory.GetDonor(id);

                    modelDto = new DonorModelDto()
                    {
                        DonorID = model.Item1.DonorID,
                        FirstName = model.Item1.FirstName,
                        LastName = model.Item1.LastName,
                        Address = model.Item1.Address,
                        Email = model.Item1.Email,
                        Phone = model.Item1.Phone,
                        BloodType = model.Item1.BloodType,
                        Gender = model.Item1.Gender.ToString(),
                        BloodStock = model.Item2,
                        Age = model.Item1.Age
                    };
                }

                return Ok(modelDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDonor(string username, string password, [FromBody]DonorModelDto modelDto, [FromHeader] string token)
        {
            var validationResult = JWTAuth.ValidateCurrentToken(token);

            var claim = String.Empty;

            if (!validationResult) return Ok(false);

            claim = JWTAuth.GetClaim(token, "UserRole");

            Enum.TryParse(modelDto.Gender, out Gender gender);
            var success = false;

            try
            {
                if(claim.Equals("Admin"))
                {
                    var model = new DonorModel()
                    {
                        DonorID = modelDto.DonorID,
                        FirstName = modelDto.FirstName,
                        LastName = modelDto.LastName,
                        Address = modelDto.Address,
                        Email = modelDto.Email,
                        Phone = modelDto.Phone,
                        BloodType = modelDto.BloodType,
                        Gender = gender,
                        Age = modelDto.Age
                    };

                    success = await _donorFactory.PostDonor(username, password, model, false);
                }

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditDonor(DonorModelDto modelDto)
        {
            Enum.TryParse(modelDto.Gender, out Gender gender);

            try
            {
                var model = new DonorModel()
                {
                    DonorID = modelDto.DonorID,
                    FirstName = modelDto.FirstName,
                    LastName = modelDto.LastName,
                    Address = modelDto.Address,
                    Email = modelDto.Email,
                    Phone = modelDto.Phone,
                    BloodType = modelDto.BloodType,
                    Gender = gender,
                    Age = modelDto.Age
                };

                var success = await _donorFactory.EditDonor(model);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(Guid id)
        {
            try
            {
                var success = await _donorFactory.DeleteDonor(id);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    #region Models
    public class DonorModelDto
    {
        public Guid DonorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
        public string Gender { get; set; }
        public int BloodStock { get; set; }
    }

    #endregion
}
