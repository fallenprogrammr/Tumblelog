using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tumblelog.Models {
        public class Post {
            [Key]
            public int PostId { get; set; }
            [DataType(DataType.Text)]
            public string Slug { get; set; }
            [DataType(DataType.Text)]
            public string Title { get; set; }
            [DataType(DataType.Text)]
            public string Body { get; set; }
            [DataType(DataType.DateTime)]
            public DateTime CreatedOn { get; set; }
        }

    public class HomePageModel {
        public List<Post> LatestPosts {get;set;}
        public int TotalPosts { get; set; }

        public HomePageModel() {
            LatestPosts = new List<Post>();
        }
    }
}