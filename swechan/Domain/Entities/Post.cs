using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string ImagePath { get; set; }
        public string Text { get; set; }
        public DateTime CeatedAt { get; set; }

        public int UserId { get; set; }
        public int ThreadId { get; set; }
        public virtual User User { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
