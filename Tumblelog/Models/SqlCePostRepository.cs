using System.Configuration;
using System.Data.SqlServerCe;
namespace Tumblelog.Models {
    public class SqlCePostRepository : IPostRepository{

        public HomePageModel GetHomePageModel() {
            //SqlServerCe does not support multiple resultsets or subqueries, hence the running of 2 seperate queries in this example (my sql-fu is rusty maybe?).
            var homePageModel = new HomePageModel();
            using (var sqlCeConnection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["tumblelogDb"].ConnectionString)) {
                sqlCeConnection.Open();
                const string getAllPostsQuery = "SELECT TOP 5 postid, title, body, slug, created_on FROM posts ORDER BY created_on DESC;";
                const string getTotalPostsCountQuery = "SELECT COUNT(*) as total_posts FROM posts;";
                using (var getAllPostsCommand = new SqlCeCommand(getAllPostsQuery, sqlCeConnection)) {
                    using (var allPostsReader = getAllPostsCommand.ExecuteReader()) {
                        while (allPostsReader.Read()) {
                            homePageModel.LatestPosts.Add(new Post() {
                                PostId = allPostsReader.GetInt32(allPostsReader.GetOrdinal("postid")),
                                Slug = allPostsReader.GetString(allPostsReader.GetOrdinal("slug")),
                                Title = allPostsReader.GetString(allPostsReader.GetOrdinal("title")),
                                Body = allPostsReader.GetString(allPostsReader.GetOrdinal("body")),
                                CreatedOn = allPostsReader.GetDateTime(allPostsReader.GetOrdinal("created_on"))
                            });
                        }
                    }
                }
                using (var getTotalPostsCommand = new SqlCeCommand(getTotalPostsCountQuery,sqlCeConnection)) {
                    using (var totalPostsReader = getTotalPostsCommand.ExecuteReader()) {
                        if (totalPostsReader.Read()) {
                            homePageModel.TotalPosts =
                                totalPostsReader.GetInt32(totalPostsReader.GetOrdinal("total_posts"));
                        }
                    }
                }
            }
            return homePageModel;
        }
    }
}