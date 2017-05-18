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

namespace TournamentWizard.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SportEventContext _context;

        public MainViewModel()
        {
            _context = new SportEventContext();
        }

        #region BindingProperies

        public SportEventContext Context
        {
            get { return _context; }
        }

        #endregion

        #region Commands

        private SaveCommand _saveCommand = new SaveCommand();

        public SaveCommand SaveCommand
        {
            get { return _saveCommand; }
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
