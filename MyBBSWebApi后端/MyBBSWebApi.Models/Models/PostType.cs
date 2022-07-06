using System;
using System.Collections.Generic;

#nullable disable

namespace MyBBSWebApi.Models.Models
{
    public partial class PostType
    {
        public int Id { get; set; }
        public string PostType1 { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserId { get; set; }
    }
}
