using Microsoft.Extensions.Logging;

using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;

namespace Postify.Infrastructure.Persistance
{
    public static class ApplicationDbContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var userId1 = Guid.NewGuid();
                var userId2 = Guid.NewGuid();
                var userId3 = Guid.NewGuid();
                var userId4 = Guid.NewGuid();

                var postId1 = Guid.NewGuid();
                var postId2 = Guid.NewGuid();
                var postId3 = Guid.NewGuid();

                var commentId1 = Guid.NewGuid();
                var commentId2 = Guid.NewGuid();
                var commentId3 = Guid.NewGuid();
                var commentId4 = Guid.NewGuid();
                var commentId5 = Guid.NewGuid();
                var commentId6 = Guid.NewGuid();
                var commentId7 = Guid.NewGuid();
                var commentId8 = Guid.NewGuid();
                var commentId9 = Guid.NewGuid();

                var postLikeId1 = Guid.NewGuid();
                var postLikeId2 = Guid.NewGuid();
                var postLikeId3 = Guid.NewGuid();
                var postLikeId4 = Guid.NewGuid();
                var postLikeId5 = Guid.NewGuid();
                var postLikeId6 = Guid.NewGuid();
                var postLikeId7 = Guid.NewGuid();
                var postLikeId8 = Guid.NewGuid();
                var postLikeId9 = Guid.NewGuid();

                var commentLikeId1 = Guid.NewGuid();
                var commentLikeId2 = Guid.NewGuid();
                var commentLikeId3 = Guid.NewGuid();
                var commentLikeId4 = Guid.NewGuid();
                var commentLikeId5 = Guid.NewGuid();
                var commentLikeId6 = Guid.NewGuid();
                var commentLikeId7 = Guid.NewGuid();
                var commentLikeId8 = Guid.NewGuid();

                var cl_p1_c1 = new List<CommentLike>();
                var cl_p1_c2 = new List<CommentLike>();
                var cl_p2_c3 = new List<CommentLike>();
                var cl_p2_c9 = new List<CommentLike>();
                var cl_p3_c5 = new List<CommentLike>();
                var cl_p3_c7 = new List<CommentLike>();
                var cl_p3_c8 = new List<CommentLike>();

                var cl_u1 = new List<CommentLike>();
                var cl_u2 = new List<CommentLike>();
                var cl_u3 = new List<CommentLike>();

                var pl_u1 = new List<PostLike>();
                var pl_u2 = new List<PostLike>();
                var pl_u3 = new List<PostLike>();
                var pl_u4 = new List<PostLike>();

                var pl_p1 = new List<PostLike>();
                var pl_p2 = new List<PostLike>();
                var pl_p3 = new List<PostLike>();

                var c_p1 = new List<Comment>();
                var c_p2 = new List<Comment>();
                var c_p3 = new List<Comment>();

                var p_u1 = new List<Post>();
                var p_u2 = new List<Post>();

                var c_u1 = new List<Comment>();
                var c_u2 = new List<Comment>();
                var c_u3 = new List<Comment>();
                var c_u4 = new List<Comment>();

                var users = new List<User>();
                var posts = new List<Post>();
                var comments = new List<Comment>();
                var postLikes = new List<PostLike>();
                var commentLikes = new List<CommentLike>();
                var friendships = new List<Follower>();

                var commentLike1 = new CommentLike(userId1, commentId1); // p1
                var commentLike2 = new CommentLike(userId2, commentId1); // p1
                var commentLike3 = new CommentLike(userId3, commentId2); // p1
                var commentLike4 = new CommentLike(userId1, commentId3); // p2
                var commentLike9 = new CommentLike(userId1, commentId9); // p2
                var commentLike10 = new CommentLike(userId3, commentId9); // p2
                var commentLike5 = new CommentLike(userId2, commentId5); //p3
                var commentLike6 = new CommentLike(userId2, commentId7); //p3
                var commentLike7 = new CommentLike(userId2, commentId8); //p3
                var commentLike8 = new CommentLike(userId1, commentId8); //p3

                var postLike1 = new PostLike(userId1, postId1); // p1
                var postLike2 = new PostLike(userId3, postId1); // p1
                var postLike3 = new PostLike(userId4, postId1); // p1
                var postLike4 = new PostLike(userId2, postId2); //p2
                var postLike5 = new PostLike(userId3, postId2); //p2
                var postLike6 = new PostLike(userId1, postId3); //p3
                var postLike7 = new PostLike(userId2, postId3); //p3
                var postLike8 = new PostLike(userId3, postId3); //p3
                var postLike9 = new PostLike(userId4, postId3); //p3

                cl_u1.Add(commentLike1);
                cl_u1.Add(commentLike4);
                cl_u1.Add(commentLike9);
                cl_u1.Add(commentLike8);
                cl_u2.Add(commentLike2);
                cl_u2.Add(commentLike5);
                cl_u2.Add(commentLike6);
                cl_u2.Add(commentLike7);
                cl_u3.Add(commentLike3);
                cl_u3.Add(commentLike10);

                cl_p1_c1.Add(commentLike1);
                cl_p1_c1.Add(commentLike2);
                cl_p1_c2.Add(commentLike3);
                cl_p2_c3.Add(commentLike4);
                cl_p2_c9.Add(commentLike9);
                cl_p2_c9.Add(commentLike10);
                cl_p3_c5.Add(commentLike5);
                cl_p3_c7.Add(commentLike6);
                cl_p3_c8.Add(commentLike7);
                cl_p3_c8.Add(commentLike8);

                pl_p1.Add(postLike1);
                pl_p1.Add(postLike2);
                pl_p1.Add(postLike3);
                pl_p2.Add(postLike4);
                pl_p2.Add(postLike5);
                pl_p3.Add(postLike6);
                pl_p3.Add(postLike7);
                pl_p3.Add(postLike8);
                pl_p3.Add(postLike9);

                var comment1 = new Comment(commentId1, null, "Comment description 1", cl_p1_c1, postId1, userId2);
                var comment2 = new Comment(commentId2, null, "Comment description 2", cl_p1_c2, postId1, userId3);
                var comment3 = new Comment(commentId3, null, "Comment description 3", cl_p2_c3, postId2, userId4);
                var comment4 = new Comment(commentId4, commentId3.ToString(), "Comment description 4", null, postId2, userId1);
                var comment9 = new Comment(commentId9, commentId4.ToString(), "Comment description 9", cl_p2_c9, postId2, userId4);
                var comment5 = new Comment(commentId5, null, "Comment description 5", cl_p3_c5, postId3, userId3);
                var comment6 = new Comment(commentId6, commentId5.ToString(), "Comment description 6", null, postId3, userId2);
                var comment7 = new Comment(commentId7, null, "Comment description 7", cl_p3_c7, postId3, userId1);
                var comment8 = new Comment(commentId8, null, "Comment description 8", cl_p3_c8, postId3, userId4);

                c_p1.Add(comment1);
                c_p1.Add(comment2);
                c_p2.Add(comment3);
                c_p2.Add(comment4);
                c_p2.Add(comment9);
                c_p3.Add(comment5);
                c_p3.Add(comment6);
                c_p3.Add(comment7);
                c_p3.Add(comment8);

                c_u1.Add(comment4);
                c_u1.Add(comment7);
                c_u2.Add(comment1);
                c_u2.Add(comment6);
                c_u3.Add(comment2);
                c_u3.Add(comment5);
                c_u4.Add(comment3);
                c_u4.Add(comment9);
                c_u4.Add(comment8);

                var post1 = new Post(postId1, "Post description 1", $"https://localhost:4200/posts/{postId1}", ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/PostImages/post-1.png"), userId1, c_p1, pl_p1);
                var post2 = new Post(postId2, "Post description 2", $"https://localhost:4200/posts/{postId2}", ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/PostImages/post-2.png"), userId1, c_p2, pl_p2);
                var post3 = new Post(postId3, "Post description 3", $"https://localhost:4200/posts/{postId3}", ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/PostImages/post-3.png"), userId2, c_p3, pl_p3);

                p_u1.Add(post1);
                p_u1.Add(post2);
                p_u2.Add(post3);

                var friendship1 = new Follower(userId2, userId1, FriendshipStatus.Accepted);
                var friendship2 = new Follower(userId3, userId1, FriendshipStatus.Rejected);
                var friendship3 = new Follower(userId1, userId2, FriendshipStatus.Accepted);
                var friendship4 = new Follower(userId3, userId2, FriendshipStatus.Rejected);
                var friendship5 = new Follower(userId1, userId4, FriendshipStatus.Pending);
                var friendship6 = new Follower(userId2, userId4, FriendshipStatus.Rejected);
                var friendship7 = new Follower(userId3, userId4, FriendshipStatus.Accepted);

                var f_u1 = new List<Follower>();
                var f_u2 = new List<Follower>();
                var f_u4 = new List<Follower>();

                f_u1.Add(friendship1);
                f_u1.Add(friendship2);
                f_u2.Add(friendship3);
                f_u2.Add(friendship4);
                f_u4.Add(friendship5);
                f_u4.Add(friendship6);
                f_u4.Add(friendship7);

                var user1 = new User(userId1, "Anthony", "Deville", "lightfelcore", "adev08@outlook.com", BCrypt.Net.BCrypt.HashPassword("Password123!!"), ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/ProfileImages/anthony.png"), c_u1, p_u1, cl_u1, pl_u1, f_u1);
                var user2 = new User(userId2, "John", "Doe", "john_doe", "john@doe.com", BCrypt.Net.BCrypt.HashPassword("Password123!!"), ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/ProfileImages/john.png"), c_u2, p_u2, cl_u2, pl_u2, f_u2);
                var user3 = new User(userId3, "Massimo", "Dutty", "md14", "massimo.dutty@gmail.com", BCrypt.Net.BCrypt.HashPassword("Password123!!"), ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/ProfileImages/massimo.png"), c_u3, null, cl_u3, pl_u3, null);
                var user4 = new User(userId4, "Elon", "Musk", "muskelon_tesla", "elon.musk@tesla.com", BCrypt.Net.BCrypt.HashPassword("Password123!!"), ConvertToByteArray("../Postify.Infrastructure/Persistance/Data/SeedData/ProfileImages/elon.png"), c_u4, null, null, pl_u4, f_u4);

                users.Add(user1);
                users.Add(user2);
                users.Add(user3);
                users.Add(user4);

                posts.Add(post1);
                posts.Add(post2);
                posts.Add(post3);

                comments.Add(comment1);
                comments.Add(comment2);
                comments.Add(comment3);
                comments.Add(comment4);
                comments.Add(comment5);
                comments.Add(comment6);
                comments.Add(comment7);
                comments.Add(comment8);

                postLikes.Add(postLike1);
                postLikes.Add(postLike2);
                postLikes.Add(postLike3);
                postLikes.Add(postLike4);
                postLikes.Add(postLike5);
                postLikes.Add(postLike6);
                postLikes.Add(postLike7);
                postLikes.Add(postLike8);
                postLikes.Add(postLike9);

                commentLikes.Add(commentLike1);
                commentLikes.Add(commentLike2);
                commentLikes.Add(commentLike3);
                commentLikes.Add(commentLike4);
                commentLikes.Add(commentLike5);
                commentLikes.Add(commentLike6);
                commentLikes.Add(commentLike7);
                commentLikes.Add(commentLike8);
                commentLikes.Add(commentLike9);
                commentLikes.Add(commentLike10);

                friendships.Add(friendship1);
                friendships.Add(friendship2);
                friendships.Add(friendship3);
                friendships.Add(friendship4);
                friendships.Add(friendship5);
                friendships.Add(friendship6);
                friendships.Add(friendship7);

                // Seeding
                if (!context.Users.Any())
                {
                    await context.Users.AddRangeAsync(users);
                    await context.SaveChangesAsync();
                }

                if (!context.Posts.Any())
                {
                    await context.AddRangeAsync(posts);
                    await context.SaveChangesAsync();
                }

                if (!context.Comments.Any())
                {
                    await context.AddRangeAsync(comments);
                    await context.SaveChangesAsync();
                }

                if (!context.PostLikes.Any())
                {
                    await context.AddRangeAsync(postLikes);
                    await context.SaveChangesAsync();
                }

                if (!context.CommentLikes.Any())
                {
                    await context.AddRangeAsync(commentLikes);
                    await context.SaveChangesAsync();
                }

                if (!context.Friendships.Any())
                {
                    await context.AddRangeAsync(friendships);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogInformation(ex.Message);
            }
        }

        private static byte[] ConvertToByteArray(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}