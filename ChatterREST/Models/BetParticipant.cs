﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatterREST.Models
{
    public class BetParticipant
    {
        public int Id { get; set; }
        [Required]
        public bool Option { get; set; }
        [Required]
        public int BetId { get; set; }
        public Bet Bet { get; set; }
        [Required, ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}