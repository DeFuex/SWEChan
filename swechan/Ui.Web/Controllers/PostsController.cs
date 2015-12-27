using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Model;
using Ui.Web.Models;


namespace Ui.Web.Controllers
{
    public class PostsController : Controller
    {
        private PostBusinessLayer postsBusinessLayer;
        private ThreadBusinessLayer threadBusinessLayer;

        public PostsController(PostBusinessLayer postsBusinessLayer,
            ThreadBusinessLayer threadBusinessLayer)
        {
            this.postsBusinessLayer = postsBusinessLayer;
            this.threadBusinessLayer = threadBusinessLayer;
        }

        public ActionResult PostsByThread(int threadId)
        {
            var vm = new List<PostViewModel>();
            foreach (Post p in postsBusinessLayer.ByThread(threadId))
            {
                vm.Add(new PostViewModel(p, new UserViewModel(p.User), new ThreadViewModel(p.Thread)));
            }
            return View("PostsByThread", vm);
        }

        [HttpGet]
        public ActionResult Create(int threadId)
        {
            return View("Create", new PostViewModel(threadId));
        }

        [HttpPost]
        public ActionResult Create(PostViewModel post)
        {
            postsBusinessLayer.createPost(post.ThreadId, post.Text, post.ImagePath);
            return RedirectToAction("PostsByThread", new { threadId = post.ThreadId });
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int postId)
        {
           return View("Edit", postsBusinessLayer.getPostById(postId));
        }

        [HttpPost]
        public ActionResult EditComplete(int postId, string imgPath, string text)
        {
            var post = postsBusinessLayer.getPostById(postId);
            post.Text = text;
            post.ImagePath = imgPath;

            postsBusinessLayer.SaveChanges();

            return RedirectToAction("PostsByThread", new { threadId = post.ThreadId });
        }

        public ActionResult Delete(int postId)
        {
            var post = postsBusinessLayer.getPostById(postId);
            postsBusinessLayer.deletePostById(post.PostId);
            return RedirectToAction("PostsByThread", new { threadId = post.ThreadId });
        }
    }
}
