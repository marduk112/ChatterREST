using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using ChatterREST.Models;

namespace ChatterREST.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}