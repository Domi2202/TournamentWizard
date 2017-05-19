using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentWizard.Models
{
    public class Team
    {
        //private Keys
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public bool LateStart { get; set; }

        //foreign Keys
        public int CompetitionID { get; set; }
        public virtual Competition Competition { get; set; }

    }
}
