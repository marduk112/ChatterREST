using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatterREST.Models
{
    public class BetCommentDTO
    {
        public int Id { get; set; }
        public string Commment { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public int BetId { get; set; }
    }
}