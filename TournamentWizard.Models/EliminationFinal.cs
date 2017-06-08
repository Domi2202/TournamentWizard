using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TournamentWizard.Models
{
    public class EliminationFinal
    {
        //private Keys
        public int EliminationFinalID { get; set; }
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        public int LastPlayoffPosition { get; set; }
        public int Tier { get; set; }

        //foreign Keys
        public int CompetitionID { get; set; }
        public virtual Competition Competition { get; set; }
    }
}
