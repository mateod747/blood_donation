using bloodDonation.DAL;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class MedicalPersonnelFactory : IMedicalPersonnelFactory
    {
        public readonly IMedicalPersonnelDAL _medicalPersonnelDAL;

        public MedicalPersonnelFactory(IMedicalPersonnelDAL medicalPersonnelDAL)
        {
            _medicalPersonnelDAL = medicalPersonnelDAL;
        }

        public async Task<MedicalPersonnelModel> GetMedicalPersonnel(int id)
        {
            return await _medicalPersonnelDAL.GetMedicalPersonnel(id);
        }

        public async Task<bool> PostMedicalPersonnel(MedicalPersonnelModel model)
        {
            return await _medicalPersonnelDAL.PostMedicalPersonnel(model);
        }

        public async Task<bool> EditMedicalPersonnel(MedicalPersonnelModel model)
        {
            return await _medicalPersonnelDAL.EditMedicalPersonnel(model);
        }

        public async Task<bool> DeleteMedicalPersonnel(int id)
        {
            return await _medicalPersonnelDAL.DeleteMedicalPersonnel(id);
        }
    }
}
