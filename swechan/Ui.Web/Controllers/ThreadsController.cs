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
    public class ThreadsController : Controller
    {
        private ThreadBusinessLayer threadBusinessLayer;

        public ThreadsController(ThreadBusinessLayer threadBusinessLayer)
        {
            this.threadBusinessLayer = threadBusinessLayer;
        }

        public ActionResult Index()
        {
            var threads = threadBusinessLayer.All();
            var vm = new List<ThreadViewModel>();
            foreach(Thread t in threads) {
                vm.Add(new ThreadViewModel(t));
            }
            return View("Index", vm);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ThreadViewModel thread)
        {
            threadBusinessLayer.createThread(thread.Title);
            return Redirect("Index");
        }

        public ActionResult Edit(int threadId)
        {
            return View("Edit", threadBusinessLayer.getThreadById(threadId));
        }

        public ActionResult EditComplete(int threadId, string title)
        {
            threadBusinessLayer.getThreadById(threadId).Title = title;
            threadBusinessLayer.SaveChanges();
            return Redirect("Index");
        }

        public ActionResult Delete(int id)
        {
            threadBusinessLayer.deleteThreadById(id);
            return Redirect("Index");
        }

    }
}
