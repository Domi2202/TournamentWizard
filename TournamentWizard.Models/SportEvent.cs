using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TournamentWizard.Models
{
    public class SportEvent
    {
        public SportEvent()
        {
            Competitions = new ObservableCollection<Competition>();
        }

        //private Keys
        public int SportEventID { get; set; }
        public string Name { get; set; }
        public int FieldCount { get; set; }
        public string Description { get; set; }

        //foreign Keys
        public ObservableCollection<Competition> Competitions { get; private set; }
    }
}
