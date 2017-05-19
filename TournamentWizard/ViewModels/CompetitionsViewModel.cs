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
using MaterialDesignThemes.Wpf;
using System.Diagnostics;

namespace TournamentWizard.ViewModels
{
    class CompetitionsViewModel : INotifyPropertyChanged
    {
        public CompetitionsViewModel()
        {
            MainViewModel.ActiveSportEventChanged += ReloadView;
        }

        #region bindingProperties
        public ObservableCollection<Competition> Competitions
        {
            get
            {
                if (MainViewModel.ActiveSportEvent != null)
                {
                    return MainViewModel.ActiveSportEvent.Competitions;
                }                
                else return new ObservableCollection<Competition>();
            }
        }

        #endregion

        #region Commands
        public ICommand AddCompetition => new CustomCommand(ExecuteAddComeptition);

        public void ExecuteAddComeptition(object parameter)
        {
            MainViewModel.ActiveSportEvent.Competitions.Add(new Competition()
            {
                CompetitionName = "Neues Turnier"
            });
        }
        #endregion

        #region propertyChanges
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void ReloadView(object sender, EventArgs e)
        {
            OnPropertyChanged("Competitions");
        }
        #endregion
    }
}
