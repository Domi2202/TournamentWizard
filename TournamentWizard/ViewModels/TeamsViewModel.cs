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
    class TeamsViewModel : INotifyPropertyChanged
    {
        public TeamsViewModel()
        {
            MainViewModel.ActiveSportEventChanged += ReloadView;
        }

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

        public ICommand AddTeam => new CustomCommand(ExecuteAddTeam);

        public void ExecuteAddTeam(object parameter)
        {
            Competition comp = parameter as Competition;
            comp.Teams.Add(new Team()
            {
                TeamName = "neue Mannschaft"
            });
        }

        #region propertyChanged
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
