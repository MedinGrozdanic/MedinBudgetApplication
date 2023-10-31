using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ServiceInterfaces
{
    public interface IUserService
    {
        public Guid GetUserId(string username);
        public string Login(string userInput, string password);

        public string RegisterNewUser(string Email,
                                    string Password,
                                    string UserName,
                                    string FirstName,
                                    string LastName);

    }
}
