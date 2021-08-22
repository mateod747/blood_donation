﻿using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IBloodDonationDAL
    {
        Task<BloodDonationModel> GetBloodDonation(int id);
        Task<List<BloodDonationModel>> GetBloodDonationsAsync(int id);
        Task<bool> PostBloodDonation(BloodDonationModel model);
        Task<bool> EditBloodDonation(BloodDonationModel model);
        Task<bool> DeleteBloodDonation(int id);
    }
}
