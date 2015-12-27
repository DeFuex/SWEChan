using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace BusinessLogic
{
    public class ThreadBusinessLayer
    {

        private MyContext context;

        public ThreadBusinessLayer(MyContext ctx)
        {
            this.context = ctx;
        }


        public void createThread(string title)
        {
            context.Threads.Add(new Thread()
            {
                CreatedAt = DateTime.Now,
                Title = title
            });

            context.SaveChanges();
        }

        public IEnumerable<Thread> All()
        {
            return context.Threads.OrderByDescending(a => a.CreatedAt);
        }

        public Thread getThreadById(int threadId)
        {
            return context.Threads
                           .Single(t => t.ThreadId == threadId);
        }
        
        public void deleteThreadById(int threadId)
        {
            context.Threads.RemoveRange(context.Threads.Where(t => t.ThreadId == threadId));
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }

}