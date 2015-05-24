using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatterREST.Models
{
    public class BetParticipantDTO
    {
        public int Id { get; set; }
        public bool Option { get; set; }
        public int BetId { get; set; }
        public string UserName { get; set; }
    }
}