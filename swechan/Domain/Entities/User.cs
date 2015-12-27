using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RegistrationKey { get; set; }

        //public bool isAdmin { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
