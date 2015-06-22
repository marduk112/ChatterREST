using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatterREST.Models
{
    public class Reward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
    }
}