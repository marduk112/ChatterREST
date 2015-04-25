using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatterREST.Models
{
    public class UserPoint
    {
        public int Id { get; set; }
        [Required, DefaultValue(1000)]
        public int Point { get; set; }
        [Required, ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}