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
        private readonly PostService _postService;

        ViewAsViewModel ViewAsvm = new ViewAsViewModel();

        public HomeController(ILogger<HomeController> logger, UserService userService, PostService postService)
        {
            _logger = logger;
            _userService = userService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var userService = new UserService();
            //var postService = new PostService();
            var userViewModel = new SocialNetworkViewModel
            {
                users = userService.Get(),
                //posts = postService.Get()
            };

            return View(userViewModel);
        }

        [BindProperty]
        public Inputmodel Input { get; set; }

        public class Inputmodel
        {
            public string Name { get; set; }
            public string UserIdToFollow { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCircle(string id)
        {
            if (ModelState.IsValid)
            {
                var newCircle = new Circle()
                {
                    Name = Input.Name,
                    UserIds = new List<string>()
                };

                var user = _userService.Get(id);

                //newCircle.UserIds.Add(id); // UserId not set to instance of object
                if (user.Circles == null)
                {
                    user.Circles = new List<Circle>();
                }
                user.Circles.Add(newCircle);

                _userService.Update(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        } /// <summary>
        /// Det her lort virker ikke
        /// </summary>
        /// <returns> Sprøg Henrik om hjælp</returns>

        public IActionResult CreateCircle()
        {
            return View();
        }

        public IActionResult ViewCircles(string id)
        {
            var vm = new UserViewModel();
            //var postService = new PostService();
            var user = _userService.Get(id);
            vm.UserId = user.Id;
            vm.Circles = user.Circles;

            return View(vm);
        }

        public IActionResult EditCircle(string id, string circleName)
        {
            var user = _userService.Get(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCircle(string id, string circleName, User user)
        {
            var UserToUpdate = _userService.Get(id);
            if (UserToUpdate.Circles == null)
            {
                UserToUpdate.Circles = new List<Circle>();
            }
            foreach (var c in UserToUpdate.Circles)
            {
                if (c.Name == circleName)
                {
                    if (c.UserIds == null)
                    {
                        c.UserIds = new List<string>();
                    }
                    c.UserIds.Add(user.UserToAddToCircle);
                    _userService.Update(id, UserToUpdate);
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FollowUser(string id)
        {
            User user = _userService.Get(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult FollowUser(string id, User user)
        {
            var UserToUpdate = _userService.Get(id);
            if (UserToUpdate.FollowingUserIds == null)
            {
                UserToUpdate.FollowingUserIds = new List<string>();
            }
            UserToUpdate.FollowingUserIds.Add(user.UserToFollow);

            _userService.Update(id, UserToUpdate);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Feed(string id)
        {
            User user = _userService.Get(id);
            var vm = new SocialNetworkViewModel();
            var feedvm = new FeedViewModel();
            feedvm.user = user;
            feedvm.posts = new List<Post>();
            vm.posts = _postService.Get();

            foreach (var p in vm.posts)
            {
                if (_userService.IsCreaterInFollowingList(id, p.CreatedBy))
                {
                    foreach (var userId in p.PCircle.UserIds)
                    {
                        if (id == userId)
                        {
                            feedvm.posts.Add(p);
                        }
                    }
                }
                
            }

            return View(feedvm);
        }

        public IActionResult Wall(FeedViewModel feedvm)
        {
           

            return View(feedvm);
        }

        public IActionResult ViewAs(string id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewAs(string id, string guestId)
        {
            ViewAsvm.guestId = guestId;
            ViewAsvm.wallUserId = id;

            User walluser = _userService.Get(ViewAsvm.wallUserId);
            var feedvm = new FeedViewModel();
            var socialvm = new SocialNetworkViewModel();
            feedvm.user = walluser;
            feedvm.posts = new List<Post>();
            socialvm.posts = _postService.Get();
            foreach (var p in socialvm.posts)
            {
                foreach (var userId in p.PCircle.UserIds)
                {
                    if ((ViewAsvm.guestId == userId) && (ViewAsvm.wallUserId == p.CreatedBy))
                    {
                        feedvm.posts.Add(p);
                    }
                }
            }
            return View("Wall", feedvm);
        }
    }
}
