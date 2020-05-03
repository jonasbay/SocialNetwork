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
    public class CommentController : Controller
    {

        private readonly CommentService _commentService;
        private readonly PostService _postService;

        public CommentController(CommentService commentService, PostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        // GET: Comment
        public ActionResult Index()
        {
            var vm = new UserViewModel();
            vm.comments = _commentService.Get();
            return View(vm);

            //return View("../Posts/Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id, Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.PostId = id;
                _commentService.Create(comment);
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}