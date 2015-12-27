using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic;
using Model;
using Ui.Web.Models;

namespace Ui.Web.Controllers
{
    public class UserController : Controller
    {
        private ThreadBusinessLayer threadBusinessLayer;
        private UserBusinessLayer userBusinessLayer;

        public UserController(UserBusinessLayer userBusinessLayer,
            ThreadBusinessLayer threadBusinessLayer)
        {
            this.userBusinessLayer = userBusinessLayer;
            this.threadBusinessLayer = threadBusinessLayer;
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(LoginModel user) 
        {
            if (ModelState.IsValid) 
            {
                if (userBusinessLayer.LoginValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Threads");
                }
                else 
                {
                    ModelState.AddModelError("", "Login Data is incorrect. Please type in the right username or password");
                }
            }

            return View(user);
        }


        
        [HttpGet]
        public ActionResult LogOut() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Threads");
        }

        [HttpGet]
        public ActionResult Registration() 
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }

        [HttpGet]
        public ActionResult LookupUser(string mail)
        {
            var users = userBusinessLayer.getUsersByMail(mail).
                Select(e => new {
                    e.Email
                }).ToArray();
            var mails = new List<string>();
            foreach (var u in users)
            {
                mails.Add(u.Email);
            }
            return Json(mails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ForgetPassword(ChangePasswordModel user)
        {
            if (ModelState.IsValid)
                if (userBusinessLayer.LoginValid(user.Email, user.OldPassword))
                {
                    userBusinessLayer.setPassword(user.Email, user.Password);
                    return View("ChangedPassword");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect. Please type in the right username or password");
                }
            {
            }
            return View();
        }

        [HttpGet]
        public ActionResult FinishRegistration(string mail)
        {
            userBusinessLayer.confirmRegistration(mail);
            ViewData["mail"] = mail;
            return View("FinishRegistration");
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel user) 
        {
            if (ModelState.IsValid)
            {
                userBusinessLayer.registerNewUser(user.UserId, user.FirstName, user.LastName, user.Email, user.Password);
                //Error
                //bool isAdmin = Convert.ToBoolean(Request.Form["Admin"]); //takes checkbox value from Form Registration.cshtml and saves it in the user specific bool variable.

                //if (user.Admin)
                //{
                //    System.Web.Security.Roles.AddUserToRole(user.FirstName, "Admin");
                //}
            }

            return RedirectToAction("Index", "Threads");
        }

        public ActionResult Usermanagement(string search)
        {
            IEnumerable<User> users;
            if (search != null)
            {
                users = userBusinessLayer.searchUser(search);
            }
            else
            {
                users = userBusinessLayer.All();
            }
            
            var vm = new List<UserViewModel>();
            foreach (var u in users)
            {
                vm.Add(new UserViewModel(u));
            }
            return View("UserManagement", vm);
        }


        [HttpGet]
        public ActionResult Edit(int userId)
        {
            return View("Edit", new UserViewModel(userBusinessLayer.getUserById(userId)));
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                userBusinessLayer.updateUser(user.UserId, user.FirstName, user.LastName, user.Email, user.Password, user.RegistrationKey);
                return RedirectToAction("UserManagement", "User");
            }
            else
            {
                return View(user);
            }
            
        }

        public ActionResult Delete(int userId)
        {
            userBusinessLayer.delete(userId);
            return RedirectToAction("UserManagement", "User");
        }

    }
}
