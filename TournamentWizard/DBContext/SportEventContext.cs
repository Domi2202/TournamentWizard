﻿using System;
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
        public SportEventContext() : base()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SportEventContext>());
        }

        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<EliminationFinal> EliminationFinals { get; set; }
    }
}
