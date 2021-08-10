﻿using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public interface IMedicalPersonnelDAL
    {
        Task<MedicalPersonnelModel> GetMedicalPersonnel(int id);
        Task<bool> PostMedicalPersonnel(MedicalPersonnelModel model);
        Task<bool> EditMedicalPersonnel(MedicalPersonnelModel model);
        Task<bool> DeleteMedicalPersonnel(int id);
    }
}