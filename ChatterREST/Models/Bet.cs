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
    public class Bet
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Title { get; set; }
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
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        [Required]
        public int RequiredPoints { get; set; }

        public bool? Result
        {
            get
            {
                return _result.HasValue
                    ? _result.Value
                    : (bool?) null;
            }
            set { _result = value; }
        }

        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        private DateTime? _dateCreated;
        private bool? _result;
    }
}