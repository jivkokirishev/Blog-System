using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System
{
    public static class Renderer
    {
        private readonly static ProfileLogic profileLogic = new ProfileLogic();
        private readonly static PostLogic postLogic = new PostLogic();

        public static void LoadData()
        {
            profileLogic.LoadUsers();
            postLogic.LoadPosts();
        }

        public static void SaveData()
        {
            profileLogic.SaveUsers();
            postLogic.SavePosts();
        }

        public static void InitializeMainMenu()
        {
            int choice = 0;

            while (choice!=4)
            {
                Console.WriteLine("Please choose one of the options:");
                Console.WriteLine("1. Log In");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Show All Posts");
                Console.WriteLine("4. Exit");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Email: ");
                            string email = Console.ReadLine();

                            Console.WriteLine("Password: ");
                            string password = Console.ReadLine();

                            bool isLoggedIn = profileLogic.LogIn(email, password);

                            if (!isLoggedIn)
                            {
                                Console.WriteLine("This user does NOT exist! Please register first.");
                                Waiter();
                            }
                            else
                            {
                                Console.WriteLine("You have benn logged in!");
                                Waiter();
                                InitializeProfileMenu();
                            }
                        }break;
                    case 2:
                        {
                            Console.WriteLine("User Name: ");
                            string name = Console.ReadLine();

                            Console.WriteLine("Email: ");
                            string email = Console.ReadLine();

                            Console.WriteLine("Password: ");
                            string password = Console.ReadLine();

                            profileLogic.Register(name, password, email);

                            Console.WriteLine("You have been successfully registered!");
                            Waiter();
                        }break;
                    case 3:
                        {
                            RenderPosts(postLogic.Posts);
                            Waiter();
                        }break;
                    case 4:
                        {
                            return;
                        }break;
                    default:
                        {
                            Console.WriteLine("You have NOT chose correct option. Please try again ...");
                        }break;
                }
            }
        }

        private static void InitializeProfileMenu()
        {
            int choice = 0;

            while (true)
            {
                Console.WriteLine("Please choose one of the options:");
                Console.WriteLine("1. Search Post By Name");
                Console.WriteLine("2. Show All Posts");
                Console.WriteLine("3. Show My Posts");
                Console.WriteLine("4. Create Post");
                Console.WriteLine("5. Add Comment To Post");
                Console.WriteLine("6. Delete Comment From Post");
                Console.WriteLine("7. Update Post");
                Console.WriteLine("8. Delete Post");
                Console.WriteLine("9. Log Out");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Write the title you want to search for:");
                            string title = Console.ReadLine();
                            Post post = postLogic.OpenPost(title);

                            
                            if (post == null)
                            {
                                Console.WriteLine("The post was NOT found!");
                                Waiter();
                            }
                            else
                            {
                                var listOfPosts = new List<Post>();
                                listOfPosts.Add(post);
                                RenderPosts(listOfPosts);
                                Waiter();
                            }
                        }break;
                    case 2:
                        {
                            RenderPosts(postLogic.Posts);
                            Waiter();
                        }
                        break;
                    case 3:
                        {
                            RenderPosts(postLogic.ListUserPosts(profileLogic.LoggedUserId));
                            Waiter();
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("This Is Your New Post");
                            Console.WriteLine("Please Write A Title:");
                            string title = Console.ReadLine();
                            Console.WriteLine("Write A Body:");
                            string body = Console.ReadLine();

                            postLogic.CreatePost(title, body, profileLogic.LoggedUserId);

                            Console.WriteLine("Your Post Is Created!");
                            Waiter();
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("This Is Your New Comment");

                            Console.WriteLine("Write The Post Id You Want To Add The Comment:");
                            int postId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Please Write A Body:");
                            string body = Console.ReadLine();

                            User creator = profileLogic.Users.Where(x => x.Id == profileLogic.LoggedUserId).FirstOrDefault();
                            postLogic.AddComment(postId, creator.Name, body);

                            Console.WriteLine("Your Comment Is Created!");
                            Waiter();
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("By Doing This Steps You Will Delete A Comment!");

                            Console.WriteLine("Write The Post Id You Want To Delete The Comment From:");
                            int postId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Write The Comment Id You Want To Delete:");
                            int commId = int.Parse(Console.ReadLine());

                            postLogic.DeleteComment(postId, commId);

                            Console.WriteLine("Your Comment Is Deleted!");
                            Waiter();
                        }
                        break;
                    case 7:
                        {
                            Console.WriteLine("Updating A Post");

                            Console.WriteLine("Write The Post Id You Want To Update:");
                            int postId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Please Write A New Title:");
                            string title = Console.ReadLine();

                            Console.WriteLine("Please Write A New Body:");
                            string body = Console.ReadLine();
                            
                            postLogic.UpdatePost(title, body, postId);

                            Console.WriteLine("Your Post Is Updated!");
                            Waiter();
                        }
                        break;
                    case 8:
                        {
                            Console.WriteLine("By Doing This Steps You Will Delete A Post!");

                            Console.WriteLine("Write The Post Id You Want To Delete:");
                            int postId = int.Parse(Console.ReadLine());

                            postLogic.DeletePost(postId);

                            Console.WriteLine("Your Post Is Deleted!");
                            Waiter();
                        }
                        break;
                    case 9:
                        {
                            profileLogic.LogOut();
                            Console.WriteLine("You Have Been Logged Out!");
                            Waiter();
                            return;
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("You have NOT chose correct option. Please try again ...");
                        }
                        break;
                }
            }
        }

        private static void RenderPosts(IList<Post> posts)
        {
            foreach (var post in posts)
            {
                Console.WriteLine($"Post id: {post.Id}");

                Console.WriteLine(post.Title);
                Console.WriteLine(post.Content);

                User creator = profileLogic.Users.Where(x => x.Id == post.CreatorId).FirstOrDefault();
                Console.WriteLine($"{creator.Name} ------------------ {post.CreatedOn.ToString()}");

                Console.WriteLine("Comments:");

                var comments = post.Comments;
                for (int i = 0; i < comments.Count; i++)
                {
                    var comment = comments[i];

                    Console.WriteLine($"Comment number {i} ------------------ id: {comment.Id}");
                    Console.WriteLine(comment.Content);
                    Console.WriteLine($"{comment.CreatorName} ------------------ {comment.CreatedOn.ToString()}");

                    Console.WriteLine();
                    Console.WriteLine("#--------------------------------------#");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("#++++++++++++++++++++++++++++++++++++++#");
                Console.WriteLine();
            }
        }

        private static void Waiter()
        {
            Console.WriteLine("Press any key to clear the console and continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
