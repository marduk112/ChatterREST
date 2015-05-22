using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatterREST.Models
{
    public class BetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int RequiredPoints { get; set; }
        public bool? Result { get; set; }
        public string UserName { get; set; }
    }
}