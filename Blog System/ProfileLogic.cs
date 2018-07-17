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
        private List<User> users;

        public ProfileLogic()
        {
            this.users = new List<User>();
        }

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
            if (File.Exists("../database/users.json"))
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

        public bool LogIn(string email, string password)
        {
            foreach (var user in this.users)
            {
                if (user.Email == email && user.Password == password)
                {
                    this.IsLoggedIn = true;
                    this.LoggedUserId = user.Id;
                    return true;
                }
            }

            return false;
        }

        public void LogOut()
        {
            this.IsLoggedIn = false;
            this.LoggedUserId = 0;
        }

        public void Register(string name, string password, string email)
        {
            int id = 0;
            if (this.Users.LastOrDefault() != null)
            {
                id = this.Users.LastOrDefault().Id + 1;
            }
            
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
