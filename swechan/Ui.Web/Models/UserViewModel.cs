using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ui.Web.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string RegistrationKey { get; set; }

        public bool Admin { get; set; }


        public UserViewModel()
        {
        }

        public UserViewModel(User user)
        {
            this.UserId = user.UserId;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Password = user.Password;
            this.Email = user.Email;
            this.RegistrationKey = user.RegistrationKey;
        }

        public void ApplyChanges(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Password = user.Password;
            this.Email = user.Email;
            this.RegistrationKey = user.RegistrationKey;
        }
    }

    public class LoginModel {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
