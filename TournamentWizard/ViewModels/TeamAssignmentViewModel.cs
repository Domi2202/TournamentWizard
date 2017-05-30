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
    public class TeamAssignmentViewModel : INotifyPropertyChanged
    {
        private Team _team;
        private bool _unassignementMode;

        public static event EventHandler TeamAssignmentChanged;
        /// <summary>
        /// Creates a new TeamAssignmentViewModel which holds a Team and the functionality to (un)assign it to/from Groups
        /// </summary>
        /// <param name="unassignmentMode">Set unassignmentMode to true if you want to see a cross item instead of arrow/check</param>
        public TeamAssignmentViewModel(bool unassignmentMode = false)
        {
            _unassignementMode = unassignmentMode;
            if(!_unassignementMode)
            {
                TeamAssignmentChanged += CheckStateIcon;
            }
        }


        #region bindingProperties
        public Team Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public PackIcon AssignmentStateIcon
        {
            get
            {
                if(_unassignementMode)
                {
                    return new PackIcon() { Kind = PackIconKind.Close };
                }
                else
                {
                    if (Team.Group == null)
                    {
                        return new PackIcon() { Kind = PackIconKind.ArrowRight };
                    }
                    else return new PackIcon() { Kind = PackIconKind.Check };
                }
            }
        }
        #endregion

        #region commands
        public ICommand AssignGroup => new CustomCommand(ExecuteAssignGroup);

        public void ExecuteAssignGroup(object parameter)
        {
            if(parameter == null)
            {
                return;
            }
            else
            {
                Group group = parameter as Group;
                if (_team.Group != null)
                {
                    _team.Group.Teams.Remove(_team);
                }
                _team.Group = group;
                group.Teams.Add(_team);
                OnTeamAssignmentChanged();
            }
        }

        public ICommand UnassignGroup => new CustomCommand(ExecuteUnassignGroup);

        public void ExecuteUnassignGroup(object parameter)
        {
            if (_team.Group != null)
            {
                _team.Group.Teams.Remove(_team);
            }
            _team.Group = null;
            OnTeamAssignmentChanged();
        }
        #endregion

        #region propertyChanges
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        /// <summary>
        /// Calls TeamAssignmentChanged to invoke required view updates
        /// </summary>
        private void OnTeamAssignmentChanged()
        {
            TeamAssignmentChanged?.Invoke(this, new EventArgs());
        }

        private void CheckStateIcon(object sender, EventArgs e)
        {
            OnPropertyChanged("AssignmentStateIcon");
        }

        #endregion
    }
}
