using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TournamentWizard.Models
{
    public class Competition
    {
        public Competition()
        {
            Teams = new ObservableCollection<Team>();
        }

        //private Keys
        public int CompetitionID { get; set; }
        public string CompetitionName { get; set; }
        public TimeSpan MatchDuration { get; set; }

        //foreign Keys
        public int SportEventID { get; set; }
        public SportEvent SportEvent { get; set; }
        public virtual ObservableCollection<Team> Teams { get; private set; }

    }
}
