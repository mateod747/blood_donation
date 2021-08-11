using bloodDonation.Common;
using bloodDonation.Factory;
using bloodDonation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonor(int id)
        {
            try
            {
                var hash = await PasswordHash.ValidatePassword("mateo", "mateo");
                var model = await _donorFactory.GetDonor(id);

                var modelDto = new DonorModelDto()
                {
                    DonorID = model.DonorID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                    BloodType = model.BloodType
                };

                return Ok(modelDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDonor(DonorModelDto modelDto)
        {
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
                    BloodType = modelDto.BloodType
                };

                var success = await _donorFactory.PostDonor(model);

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
                    BloodType = modelDto.BloodType
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
        public async Task<IActionResult> DeleteDonor(int id)
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
        public int DonorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
    }
    #endregion
}
