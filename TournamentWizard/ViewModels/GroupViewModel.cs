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
using TournamentWizard.Extensions;

namespace TournamentWizard.ViewModels
{
    public class GroupViewModel : INotifyPropertyChanged
    {
        private Competition _activeCompetition;
        private GroupBoxViewModel _selectedGroupVM;

        public GroupViewModel()
        {
            MainViewModel.ActiveSportEventChanged += ReloadView;
            TeamAssignmentViewModel.TeamAssignmentChanged += CheckAssignmentStatus;
            TeamAssignmentViewModel.TeamAssignmentChanged += ReloadInformationRoster;
        }

        #region bindingProperties
        public Competition ActiveCompetition
        {
            get { return _activeCompetition; }
            set
            {
                _activeCompetition = value;
                OnPropertyChanged(new[] { "Teams", "Groups", "AssignmentStatus"});
                ReloadInformationRoster();
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
        /// <summary>
        /// Returns a string that shows the number of teams assigned to a group and the total number of teams in the Group
        /// </summary>
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
        /// <summary>
        /// Get or set the Group selected in the ListBox
        /// </summary>
        public GroupBoxViewModel ActiveGroup
        {
            get { return _selectedGroupVM; }
            set
            {
                _selectedGroupVM = value;
                OnPropertyChanged("MatchesInGroup");
            }
        }

        /// <summary>
        /// Calculates the expected number of Matches in a Group
        /// </summary>
        public int MatchesInGroup
        {
            get
            {
                if (_selectedGroupVM != null)
                {
                    return _selectedGroupVM.MatchesInGroup;
                }
                return 0;
            }
        }
        /// <summary>
        /// Calculates the expected number of Matches in a Competition
        /// </summary>
        public int MatchesInCompetition
        {
            get
            {
                try
                {
                    return GroupSimCalcutaltions.ExpectedNumberOfGroupMatches(_activeCompetition);
                }
                catch { }
                return 0;
            }
        }
        /// <summary>
        /// returns the field count of the active SportEvent
        /// </summary>
        public int FieldCount
        {
            get
            {
                try
                {
                    return MainViewModel.ActiveSportEvent.FieldCount;
                }
                catch { }
                return 0;
            }         
        }
        /// <summary>
        /// returns the number of time slices needed for the current expected number of matches
        /// </summary>
        public double TimeSlicesNeededForCompetition
        {
            get
            {
                try
                {
                    return GroupSimCalcutaltions.TimeSlicesNeeded(GroupSimCalcutaltions.ExpectedNumberOfGroupMatches(_activeCompetition), MainViewModel.ActiveSportEvent.FieldCount);
                }
                catch { }
                return 0;
            }
        }
        /// <summary>
        /// returns the match duration of the currently selected competition
        /// </summary>
        public TimeSpan TimeNeededPerSlice
        {
            get
            {
                try
                {
                    return _activeCompetition.MatchDuration;
                }
                catch { }
                return new TimeSpan();
            }
        }
        /// <summary>
        /// returns the TimeSpan needed for all group matches of the current competition
        /// </summary>
        public TimeSpan TimeNeededForCompetition
        {
            get
            {
                try
                {
                    int timeSlicesNeeded = (int)Math.Ceiling(GroupSimCalcutaltions.TimeSlicesNeeded(GroupSimCalcutaltions.ExpectedNumberOfGroupMatches(_activeCompetition), MainViewModel.ActiveSportEvent.FieldCount));
                    return TimeSpanExtension.Multiply(_activeCompetition.MatchDuration, timeSlicesNeeded);
                }
                catch { }
                return new TimeSpan();
            }
        }

        public int MatchesInSportEvent
        {
            get
            {
                try
                {
                    return GroupSimCalcutaltions.ExpectedNumberOfGroupMatches(MainViewModel.ActiveSportEvent);
                }
                catch { }
                return 0;
            }
        }

        public TimeSpan SportEventGroupMatchesDuration
        {
            get
            {
                try
                {
                    return GroupSimCalcutaltions.TotalTimeNeededForGroupMatches(MainViewModel.ActiveSportEvent);
                }
                catch { }
                return new TimeSpan();
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
            OnPropertyChanged(new[] { "Competitions",});
            ReloadInformationRoster();
        }
        /// <summary>
        /// Checks the current number of assigned teams
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAssignmentStatus(object sender, EventArgs e)
        {
            OnPropertyChanged(new[] {
                "AssignmentStatus",
                "MatchesInGroup"
            });
        }
        /// <summary>
        /// Reloads the whole right hand roster fields
        /// </summary>
        private void ReloadInformationRoster()
        {
            OnPropertyChanged(new[] {
                "MatchesInGroup",
                "MatchesInCompetition",
                "FieldCount",
                "TimeSlicesNeededForCompetition" ,
                "TimeNeededPerSlice",
                "TimeNeededForCompetition",
                "MatchesInSportEvent",
                "SportEventGroupMatchesDuration"
            });
        }
        /// <summary>
        /// Reloads the whole right hand roster fields as an EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadInformationRoster(object sender, EventArgs e)
        {
            ReloadInformationRoster();
        }

        
        #endregion
    }
}
