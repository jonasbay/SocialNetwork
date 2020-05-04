using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;


        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var userService = new UserService();
            //var postService = new PostService();
            var userViewModel = new UserViewModel
            {
                users = userService.Get(),
                //posts = postService.Get()
            };

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCircle(string id, User user)
        {
            if (ModelState.IsValid)
            {
                var newCircle = new Circle()
                {
                    Name = "Randi",
                };

                newCircle.UserIds.Add(id); // UserId not set to instance of object

                user.Circles.Add(newCircle);

                _userService.Update(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        } /// <summary>
        /// Det her lort virker ikke
        /// </summary>
        /// <returns> Sprøg Henrik om hjælp</returns>

        public IActionResult CreateCircle()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
