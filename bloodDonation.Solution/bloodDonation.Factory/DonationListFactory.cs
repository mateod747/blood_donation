using bloodDonation.DAL;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class DonationListFactory : IDonationListFactory
    {
        private readonly IBloodDonationDAL _bloodDonationDAL;
        private readonly IBloodTransactionDAL _bloodTransactionDAL;
        private readonly IRecipientDAL _recipientDAL;
        private readonly IMedicalPersonnelDAL _medicalPersonnelDAL;

        public DonationListFactory(IBloodDonationDAL bloodDonationDAL, IBloodTransactionDAL bloodTransactionDAL, IRecipientDAL recipientDAL, IMedicalPersonnelDAL medicalPersonnelDAL)
        {
            _bloodDonationDAL = bloodDonationDAL;
            _bloodTransactionDAL = bloodTransactionDAL;
            _recipientDAL = recipientDAL;
            _medicalPersonnelDAL = medicalPersonnelDAL;
        }

        public async Task<DonationListDto> GetDonationListAsync(int page, int pageSize, Guid id)
        {
            var donations = new DonationListDto()
            {
                Donations = new List<DonationDto>()
            };

            var allBloodDonations = await _bloodDonationDAL.GetBloodDonationsAsync(id);

            foreach(var x in allBloodDonations)
            {
                var bloodTransaction = await _bloodTransactionDAL.GetBloodTransaction(x.BloodID);
                var recipient = await _recipientDAL.GetRecipient(bloodTransaction.RecipientID);
                var personnel = await _medicalPersonnelDAL.GetMedicalPersonnel(bloodTransaction.EmpID);

                donations.Donations.Add(
                    new DonationDto()
                    {
                        DateDonated = x.DateDonated.ToString(),
                        Quantity = bloodTransaction.Quantity,
                        Hemoglobin = bloodTransaction.Hemoglobin,
                        BloodPressure = bloodTransaction.BloodPressure,
                        Notes = bloodTransaction.Notes,
                        Success = bloodTransaction.Success,
                        DateOut = bloodTransaction.DateOut.ToString(),
                        PersonnelName = $"{personnel.FirstName} {personnel.LastName}",
                        PersonnelWorkPhone = personnel.Phone,
                        RecipientName = $"{recipient.Name}",
                        RecipientBloodType = recipient.BloodType,
                        Anon = recipient.Anon
                    }
                );
            }

            var listCount = donations.Donations.Count;
            var skipCount = pageSize * (page - 1);

            donations.ListCount = listCount;
            donations.SuccessCount = donations.Donations.Where(x => x.Success == true).ToList().Count;
            donations.Donations = donations.Donations.OrderByDescending(x => x.DateDonated).ToList();
            donations.Donations = donations.Donations.Skip(skipCount).Take(pageSize).ToList();
            return donations;
        }

        #region Models

        public class DonationListDto
        {
            public int ListCount { get; set; }
            public int SuccessCount { get; set; }
            public List<DonationDto> Donations { get; set; }
        }

        public class DonationDto
        {
            // donator part
            public string DateDonated { get; set; }
            public int Quantity { get; set; }
            public int Hemoglobin { get; set; }
            public string BloodPressure { get; set; }
            public string Notes { get; set; }
            public bool Success { get; set; }

            // medical personnel
            public string PersonnelName { get; set; }
            public string PersonnelWorkPhone { get; set; }

            // recipient part
            public string RecipientName { get; set; }
            public string RecipientBloodType { get; set; }
            public string DateOut { get; set; }
            public Anon Anon { get; set; }
        }

        #endregion
    }
}
