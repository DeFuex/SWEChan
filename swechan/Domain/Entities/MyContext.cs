using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MyContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }

        public MyContext()
            : base()
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany<Post>(user => user.Posts)
                                       .WithRequired(post => post.User)
                                       .HasForeignKey(post => post.UserId)
                                       .WillCascadeOnDelete(true);

            modelBuilder.Entity<Thread>().HasMany<Post>(thread => thread.Posts)
                                       .WithRequired(post => post.Thread)
                                       .HasForeignKey(post => post.ThreadId)
                                       .WillCascadeOnDelete(true);
        }
    }
}
