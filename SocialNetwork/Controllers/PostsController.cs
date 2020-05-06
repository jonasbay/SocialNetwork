using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public PostsController(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
    }

        // GET: Posts
        public ActionResult Index()
        {
            var vm = new SocialNetworkViewModel();
            vm.posts = _postService.Get();
            return View(vm);
        }

        // GET: Posts/Create
        public ActionResult Create(string createdBy)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post, string createdBy)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Get(createdBy);
                foreach (var c in user.Circles)
                {
                    if (c.Name == post.PostToCircle)
                    {
                        post.PCircle = c;

                    }
                }
                post.CreatedBy = createdBy;
                _postService.Create(post);
                post.Likes = 0;
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public ActionResult Like(string id)
        {
            var post = _postService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            post.Likes++;
            _postService.Update(id, post);

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult Like(string id)
        //{
        //    var post = _postService.Get(id);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    post.Likes++;
        //    _postService.Update(id, post);

        //    return RedirectToAction(nameof(Index));
        //}


        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var post = _postService.Get(id);

                if (post == null)
                {
                    return NotFound();
                }

                _postService.Remove(post.Id);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}