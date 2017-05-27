using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TournamentWizard.Models;
using TournamentWizard.DBContext;
using TournamentWizard.Commands;
using System.Windows.Input;
using System.Diagnostics;

namespace TournamentWizard.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SportEventContext _context;

        public static SportEvent ActiveSportEvent;
        public static event EventHandler ActiveSportEventChanged;

        public MainViewModel()
        {
            _context = new SportEventContext();
            _context.SportEvents.Load();
            _context.Competitions.Load();
            _context.Teams.Load();      

            Features = new[]
            {
                new Feature("Veranstaltungen", new SportEventControl() {DataContext = new SportEventsViewModel(_context, ExecuteLoadSportEvent) }),
                new Feature("Turniere", new CompetitionControl() {DataContext = new CompetitionsViewModel() }),
                new Feature("Mannschaften", new TeamControl() {DataContext = new TeamsViewModel() })
            };
        }

        #region BindingProperies

        public SportEventContext Context
        {
            get { return _context; }
        }

        public Feature[] Features { get; }

        public string HeaderBarText
        {
            get
            {
                if (ActiveSportEvent == null)
                {
                    return "Tournament Wizard";
                }
                else return ActiveSportEvent.Name;
            }
        }
        #endregion

        #region Commands

        public ICommand SaveCommand => new CustomCommand(ExecuteSave);

        public void ExecuteSave(object parameter)
        {
            var context = parameter as DBContext.SportEventContext;
            context.SaveChanges();
        }

        public void ExecuteLoadSportEvent(object parameter)
        {
            var sportEvent = parameter as SportEvent;
            ActiveSportEvent = sportEvent;
            OnPropertyChanged("HeaderBarText");
            OnActiveSportEventChanged();

        }

        private void OnActiveSportEventChanged()
        {
            ActiveSportEventChanged?.Invoke(this, new EventArgs());
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
