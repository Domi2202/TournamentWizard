using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TournamentWizard.Models
{
    public class Group
    {
        public Group()
        {
            Teams = new ObservableCollection<Team>();
        }

        //private Keys
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        //foreign Keys
        public Competition Competition { get; set; }
        public int CompetitionID { get; set; }
        public virtual ObservableCollection<Team> Teams { get; private set; }

    }
}
