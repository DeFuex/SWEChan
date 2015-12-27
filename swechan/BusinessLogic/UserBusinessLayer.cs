using System.Collections.Generic;
using System.Linq;
using Model;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic
{
    public class UserBusinessLayer
    {
        private MyContext context;

        public UserBusinessLayer(MyContext ctx)
        {
            this.context = ctx;
        }


        public void registerNewUser(int userId, string firstname, string lastname, string email, string password)
        {
            var sysUser = context.Users.Create();
                
            sysUser.FirstName = firstname;
            sysUser.LastName = lastname;
            sysUser.Email = email;
            sysUser.Password = password;
            sysUser.RegistrationKey = email;
            sysUser.UserId = userId; //unique id not possible, because Guint = 128bit and int = 32bit

            context.Users.Add(sysUser);
            context.SaveChanges();
        }

        public void confirmRegistration(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            user.RegistrationKey = "";
            context.SaveChanges();
        }

        public bool LoginValid(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.RegistrationKey == "");

            if (user != null)
            {
                return true;
            } else {

                return false;
            }
        }

        public IEnumerable<User> All() 
        {
            return context.Users.OrderByDescending(u => u.LastName);
        }

        public User getUserById(int userId) 
        {
            return context.Users
                           .Single(u => u.UserId == userId);
        }

        public IEnumerable<User> searchUser(string username)
        {
            return context.Users.Where(u => u.LastName.Contains(username)).OrderByDescending(u => u.LastName);
        }

        public void delete(int userId) 
        {
            context.Users.RemoveRange(context.Users.Where(u => u.UserId == userId));
            context.SaveChanges();
        }

        public IEnumerable<User> AutoCompleteUserByName(string name)
        {
            return context.Users.Where(u => u.LastName.StartsWith(name)).ToArray();
        }

        //Constructor Methods with needed return values in the MVC Controller and View
        public class UserList // ViewModel
        {
            public IEnumerable<User> Users { get; private set; }

            public UserList(IEnumerable<User> users)
            {
                Users = users;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public void setPassword(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            user.Password = password;
            context.SaveChanges();
        }

        public IEnumerable<User> getUsersByMail(string mail)
        {
            return context.Users.Where(user => user.Email.Contains(mail));
        }

        public void updateUser(int userId, string firstname, string lastname, 
            string email, string password, string registration)
        {

            var user = this.getUserById(userId);
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            user.Password = password;
            user.RegistrationKey = registration;

            context.SaveChanges();

        }
    }
}