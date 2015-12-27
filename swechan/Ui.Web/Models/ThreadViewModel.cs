using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Ui.Web.Models
{
    public class ThreadViewModel
    {
        public int ThreadId { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public ThreadViewModel()
        {
        }

        public ThreadViewModel(Thread thread)
        {
            this.ThreadId = thread.ThreadId;
            this.Title = thread.Title;
            this.CreatedAt = thread.CreatedAt;
        }

        public void ApplyChanges(Thread thread)
        {
            this.ThreadId = thread.ThreadId;
            this.Title = thread.Title;
            this.CreatedAt = thread.CreatedAt;
        }
    }
}
