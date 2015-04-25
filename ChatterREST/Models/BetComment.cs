using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatterREST.Models
{
    public class BetComment
    {
        public int Id { get; set; }
        [Required]
        public string Commment { get; set; }
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated.HasValue
                   ? _dateCreated.Value
                   : DateTimeOffset.UtcNow.UtcDateTime;
            }

            set { _dateCreated = value; }
        }
        [Required]
        public int BetId { get; set; }
        public Bet Bet { get; set; }
        [Required, ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        private DateTime? _dateCreated;
    }
}