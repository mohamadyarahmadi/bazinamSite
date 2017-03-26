using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazinamSite2.Models
{
    public class News
    {
        public News()
        {
            PicturescCollection=new List<Picture>();
            CommentsCollection=new List<Comment>();
        }

        public int NewsID { get; set; }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public virtual ICollection<Picture> PicturescCollection { get; set; }
        public virtual ICollection<Comment> CommentsCollection { get; set; }
    }
    public class Picture
    {
        
        public int PictureID { get; set; }

        public string PicName { get; set; }
        public string PicUrl { get; set; }
        public bool IsRefrence { get; set; }
        public byte[] PicSourceBytes { get; set; }
        public virtual ICollection<News> NewscCollection { get; set; }


    }

    public class Comment
    {
        
        public int CommentID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string comment { get; set; }
        public bool IsAllowed { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual News NewsModel { get; set; }
    }
}