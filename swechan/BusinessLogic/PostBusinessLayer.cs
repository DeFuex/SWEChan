using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace BusinessLogic
{
    public class PostBusinessLayer
    {
        private MyContext context;

        public PostBusinessLayer(MyContext ctx)
        {
            this.context = ctx;
        }

        public IEnumerable<Post> FindLastThreePosts()
        {
            return context.Posts
                           .OrderByDescending(c => c.PostId)
                           .Take(3)
                           .ToArray();
        }

        public IEnumerable<Post> ByThread(int threadId)
        {
            var thread = context.Threads
                           .Include("Posts")
                           .Single(t => t.ThreadId == threadId);
            var posts = thread.Posts.ToArray();

            return posts;
        }

        public void createPost(int threadId, string text, string imgPath)
        {
            context.Posts.Add(new Post()
            {
                CeatedAt = DateTime.Now,
                Text = text,
                ThreadId = threadId,
                UserId = context.Users.First().UserId,
                ImagePath = imgPath
            });

            context.SaveChanges();
        }

        public Post getPostById(int postId)
        {
            return context.Posts
                           .Single(p => p.PostId == postId);
        }

        public void deletePostById(int postId)
        {
            context.Posts.RemoveRange(context.Posts.Where(p => p.PostId == postId));
            context.SaveChanges();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }


}