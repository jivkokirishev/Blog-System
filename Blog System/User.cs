using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System
{
    public class User
    {
        public User(int id, string name, string password, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
