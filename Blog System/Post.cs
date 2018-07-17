using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System
{
    public class Post
    {
        public Post(int id, string title, string content, int crId)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.CreatorId = crId;
            this.Comments = new List<Comment>();
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; private set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CreatorId { get; set; }

        public List<Comment> Comments { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
