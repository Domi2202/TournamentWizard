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
    public class GroupViewModel : INotifyPropertyChanged
    {
        private Competition _activeCompetition;

        public GroupViewModel()
        {
            MainViewModel.ActiveSportEventChanged += ReloadView;
            TeamAssignmentViewModel.TeamAssignmentChanged += CheckAssignmentState;
        }

        #region bindingProperties
        public Competition ActiveCompetition
        {
            get { return _activeCompetition; }
            set
            {
                _activeCompetition = value;
                OnPropertyChanged(new[] { "Teams", "Groups", "AssignmentStatus" });
            }
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

        public ObservableCollection<TeamAssignmentViewModel> Teams
        {
            get
            {
                if (_activeCompetition != null)
                {
                    ObservableCollection<TeamAssignmentViewModel> teamItems = new ObservableCollection<TeamAssignmentViewModel>();
                    foreach (Team team in _activeCompetition.Teams)
                    {
                        teamItems.Add(new TeamAssignmentViewModel() { Team = team });
                    }
                    return teamItems;
                }
                else return new ObservableCollection<TeamAssignmentViewModel>();
            }
        }

        /// <summary>
        /// Returns a set of ViewModels for all Groups of the currently active Competition
        /// </summary>
        public ObservableCollection<GroupBoxViewModel> Groups
        {
            get
            {
                ObservableCollection<GroupBoxViewModel> groupItems = new ObservableCollection<GroupBoxViewModel>();
                if (_activeCompetition != null)
                {                    
                    foreach(Group group in _activeCompetition.Groups)
                    {
                        groupItems.Add(new GroupBoxViewModel() { Group = group });
                    }
                }
                return groupItems;
            }
        }

        public string AssignmentStatus
        {
            get
            {
                if (_activeCompetition != null)
                {
                    int total = _activeCompetition.Teams.Count;
                    int assigned = 0;
                    assigned += _activeCompetition.Teams.Where(t => t.Group != null).Count();
                    return assigned + "/" + total;
                }
                else return "0/0";
            }
        }
        #endregion

        #region Commands
        public ICommand AddGroup => new CustomCommand(ExecuteAddGroup);

        public void ExecuteAddGroup(object parameter)
        {
            Competition comp = parameter as Competition;
            comp.Groups.Add(new Group()
            {
                GroupName = "neue Gruppe"
            });
            OnPropertyChanged("Groups");
        }

        public ICommand DeleteGroup => new CustomCommand(ExecuteDeleteGroup, CanExecuteDeleteGroup);

        public void ExecuteDeleteGroup(object parameter)
        {
            GroupBoxViewModel groupVm = parameter as GroupBoxViewModel;
            foreach (TeamAssignmentViewModel teamVm in groupVm.Teams)
            {
                teamVm.UnassignGroup.Execute(new object());
            }
            //_activeCompetition.Groups.Remove(groupVm.Group);
            MainViewModel.Context.Groups.Remove(groupVm.Group);
            
            OnPropertyChanged("Groups");
        }

        public bool CanExecuteDeleteGroup(object parameter)
        {
            return parameter != null;
        }
        #endregion

        #region propertyChanges
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the UI about a change in the Model
        /// </summary>
        /// <param name="property"></param>
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Notifies the UI about multiple changes in the Model
        /// </summary>
        /// <param name="properties"></param>
        private void OnPropertyChanged(string[] properties)
        {
            if(PropertyChanged != null)
            {
                foreach(string property in properties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }
        /// <summary>
        /// Triggers TeamAssignmentChanged to make TeamAssignmentViewModels check their state icons
        /// </summary> }
        private void ReloadView(object sender, EventArgs e)
        {
            OnPropertyChanged("Competitions");
        }
        /// <summary>
        /// Checks the current number of assigned teams
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAssignmentState(object sender, EventArgs e)
        {
            OnPropertyChanged("AssignmentStatus");
        }
        #endregion
    }
}
