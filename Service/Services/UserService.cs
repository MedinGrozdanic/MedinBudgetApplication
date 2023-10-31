using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Service.Services.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Services
{
    public class UserService : IUserService
    {
        public Guid GetUserId(string username)
        {
            using (var context = new BudgetContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                return user.UserId;
            }
        }


        public string Login(string userInput, string password)
        {
            using (var context = new BudgetContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == userInput || u.Email == userInput);
                if (user == null)
                {
                    return "Users not found";
                }
                else if (password == user.Password)
                {
                    return user.Username;
                }
                else
                    return "Password incorrect";
            }
        }

        public string RegisterNewUser(string Email,
                                    string Password,
                                    string UserName,
                                    string FirstName,
                                    string LastName)

        {
            var RegisterUser = new User()
            {
                Email = Email,
                Password = Password,
                Username = UserName,
                FirstName = FirstName,
                LastName = LastName
            };

            using (var context = new BudgetContext())
            {
                var userNameExists = context.Users.FirstOrDefault(u => u.Username == UserName);
                var userEmailExists = context.Users.FirstOrDefault(u => u.Email == Email);

                if (userEmailExists != null)
                {
                    return "Email has already been used.";
                }
                else if (userNameExists != null)
                {
                    return "Username has already been used.";
                }
                else
                {
                    try
                    {
                        context.Users.Add(RegisterUser);
                        context.SaveChanges();

                        Guid userId = RegisterUser.UserId;

                        return "Users created, returning to login page.";
                    }
                    catch (DbUpdateException ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}