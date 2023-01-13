using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UserPtoject.Interfaces;
using UserPtoject.Models;
using UserPtoject.Repositories;

namespace UserPtoject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        private readonly IValidator<User> _userValidator;

        public UserController(IUser user,IValidator<User> userValidator)
        {
            _user = user;
            _userValidator = userValidator;
        }

        public IActionResult Index()
        {
            var list = _user.GetAllUsers();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _userValidator.ValidateAndThrow(user);
                if (!ModelState.IsValid)
                {
                    _user.CreateUser(user);
                    return RedirectToAction("Index", "User");
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Remove(string id)
        {
            _user.DeleteUser(id);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var user = _user.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            try
            {
                _userValidator.ValidateAndThrow(user);
                if(ModelState.IsValid)
                {
                    _user.UpdateUser(user);
                    return RedirectToAction("Index","User");
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View(user);
        }
    }
}
