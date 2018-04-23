using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System
{
    public class Comment
    {
        public Comment(int id, string crName, string content)
        {
            this.Id = id;
            this.CreatorName = crName;
            this.Content = content;
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; private set; }

        public string CreatorName { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
