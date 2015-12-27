using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Ui.Web.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CeatedAt { get; set; }

        public int UserId { get; set; }
        public int ThreadId { get; set; }
        
        public UserViewModel User { get; set; }
        public ThreadViewModel Thread { get; set; }

        private PostViewModel post = null;

        public PostViewModel()
        {
        }

        public PostViewModel(Post post, UserViewModel user, ThreadViewModel thread){
            this.PostId = post.PostId;
            this.Text = post.Text;
            this.ImagePath = post.ImagePath;
            this.CeatedAt = post.CeatedAt;
            this.User = user;
            this.ThreadId = post.ThreadId;
            this.Thread = thread;
        }

        public PostViewModel(int threadId)
        {
            this.ThreadId = threadId;
        }

        public void ApplyChanges(Post post)
        {
            this.Text = post.Text;
            this.ImagePath = post.ImagePath;
            this.CeatedAt = post.CeatedAt;
        }
    }
}
