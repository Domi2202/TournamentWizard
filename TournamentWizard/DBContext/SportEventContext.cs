using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TournamentWizard.Models;

namespace TournamentWizard.DBContext
{
    public class SportEventContext : DbContext
    {
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
