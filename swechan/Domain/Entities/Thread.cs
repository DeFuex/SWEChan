using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Model
{
    public class Thread
    {
        public int ThreadId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
