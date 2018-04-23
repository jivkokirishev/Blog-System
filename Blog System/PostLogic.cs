using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System
{
    public class PostLogic
    {
        private List<Post> posts;
        public ReadOnlyCollection<Post> Posts
        {
            get
            {
                return posts.AsReadOnly();
            }
        }

        public void SavePosts()
        {
            if (!Directory.Exists("../database/"))
            {
                Directory.CreateDirectory("../database/");
            }

            string jsonPosts = JsonConvert.SerializeObject(this.Posts);

            File.WriteAllText("../database/posts.json", jsonPosts);
        }

        public void LoadPosts()
        {
            if (Directory.Exists("../database/posts.json"))
            {
                string jsonPosts = File.ReadAllText("../database/posts.json");
                this.posts = JsonConvert.DeserializeObject<List<Post>>(jsonPosts);
            }
        }

        public void CreatePost(string title, string content, int userId)
        {
            int id = this.Posts.Last().Id;
            Post post = new Post(id, title, content, userId);
            this.posts.Add(post);
        }

        public void DeletePost(int postId)
        {
            foreach (Post post in this.Posts)
            {
                if (post.Id == postId)
                {
                    this.posts.Remove(post);
                    break;
                }
            }
        }

        public Post OpenPost(int postId)
        {
            foreach (Post post in this.Posts)
            {
                if (post.Id == postId)
                {
                    return post;
                }
            }

            return null;
        }

        public ReadOnlyCollection<Post> ListUserPosts(int userId)
        {
            List<Post> posts = new List<Post>();
            foreach (Post post in this.Posts)
            {
                if (post.CreatorId == userId)
                {
                    posts.Add(post);
                }
            }

            return posts.AsReadOnly();
        }

        // Няма смисъл от този метод!
        //public List<Post> ListAllPosts()
        //{
        //    return null;
        //}

        public void AddComment(int postId, string creatorName, string content)
        {
            int commId = -1;

            foreach (Post post in this.posts)
            {
                if (post.Id == postId)
                {
                    commId = post.Comments.Last().Id;
                    Comment comment = new Comment(commId, creatorName, content);
                    post.Comments.Add(comment);
                }
            }
        }

        public void DeleteComment(int postId, int  commentId)
        {
            foreach (Post post in this.posts)
            {
                if (post.Id == postId)
                {
                    foreach (Comment comment in post.Comments)
                    {
                        if (comment.Id == commentId)
                        {
                            post.Comments.Remove(comment);
                        }
                    }
                }
            }
        }

    }
}
