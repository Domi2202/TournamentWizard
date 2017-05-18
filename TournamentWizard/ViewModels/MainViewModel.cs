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

namespace TournamentWizard.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SportEventContext _context;

        public MainViewModel()
        {
            _context = new SportEventContext();
            _context.SportEvents.Load();

            Features = new[]
            {
                new Feature("Veranstaltungen", new SportEventControl() {DataContext = new SportEventsViewModel(_context) }),
                new Feature("Turniere", new CompetitionControl())
            };
        }

        #region BindingProperies

        public SportEventContext Context
        {
            get { return _context; }
        }

        public Feature[] Features { get; }
        #endregion

        #region Commands

        public ICommand SaveCommand => new CustomCommand(ExecuteSave);

        public void ExecuteSave(object parameter)
        {
            var context = parameter as DBContext.SportEventContext;
            context.SaveChanges();
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
