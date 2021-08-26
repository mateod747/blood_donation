using bloodDonation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class LoginFactory : ILoginFactory
    {
        private readonly IPasswordHash _passwordHash;

        public LoginFactory(IPasswordHash passwordHash)
        {
            _passwordHash = passwordHash;
        }

        public async Task<(string, Guid, bool)> GetLoginData(string username, string password)
        {
            return await _passwordHash.ValidatePassword(username, password);
        }
    }
}
