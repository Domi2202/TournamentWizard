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
    public class GroupBoxViewModel : INotifyPropertyChanged
    {
        private Group _group;

        public GroupBoxViewModel()
        {
            TeamAssignmentViewModel.TeamAssignmentChanged += ReloadTeams;
        }

        #region bindingProperties
        public Group Group
        {
            get { return _group; }
            set { _group = value; }
        }
        /// <summary>
        /// Calculates the expected number of matches in the connected group
        /// </summary>
        public int MatchesInGroup
        {
            get
            {
                return GroupSimCalcutaltions.ExpectedNumberOfGroupMatches(_group);
            }
        }

        public ObservableCollection<TeamAssignmentViewModel> Teams
        {
            get
            {
                ObservableCollection<TeamAssignmentViewModel> teamItems = new ObservableCollection<TeamAssignmentViewModel>();
                foreach (Team team in _group.Teams)
                {
                    teamItems.Add(new TeamAssignmentViewModel(unassignmentMode: true) { Team = team });
                }
                return teamItems;
            }
        }
        #endregion

        #region propertyChanges
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void ReloadTeams(object sender, EventArgs e)
        {
            OnPropertyChanged("Teams");
        }
        #endregion
    }
}
