using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Blog_System
{
    public class ProfileLogic
    {
        private List<User> users = new List<User>();

        public ReadOnlyCollection<User> Users
        {
            get
            {
                return this.users.AsReadOnly();
            }
        }
        public bool IsLoggedIn { get; private set; }
        public int LoggedUserId { get; private set; }

        public void LoadUsers()
        {
            if (Directory.Exists("../database/users.json"))
            {
                string jsonUsers = File.ReadAllText("../database/users.json");
                this.users = JsonConvert.DeserializeObject<List<User>>(jsonUsers);
            }
        }

        public void SaveUsers()
        {
            if (!Directory.Exists("../database/"))
            {
                Directory.CreateDirectory("../database/");
            }

            string jsonUsers = JsonConvert.SerializeObject(this.Users);

            File.WriteAllText("../database/users.json", jsonUsers);
        }

        public void LogIn(string email, string password)
        {
            foreach (var user in this.Users)
            {
                if (user.Email == email && user.Password == password)
                {
                    this.IsLoggedIn = true;
                    this.LoggedUserId = user.Id;
                    break;
                }
            }
        }

        public void LogOut()
        {
            this.IsLoggedIn = false;
            this.LoggedUserId = 0;
        }

        public void Register(string name, string password, string email)
        {
            int id = this.Users.Last().Id;
            User user = new User(id, name, password, email);
            this.users.Add(user);
        }

        // Няма смисъл от този метод!
        //public List<User> ListAllUsers()
        //{
        //    return null;
        //}
    }
}
