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
    public class FinalRoundViewModel : INotifyPropertyChanged
    {
        private Competition _activeCompetition;

        public FinalRoundViewModel()
        {
            MainViewModel.ActiveSportEventChanged += ReloadView;
        }

        #region bindingProperties
        public Competition ActiveCompetition
        {
            get { return _activeCompetition; }
            set
            {
                _activeCompetition = value;
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
            if (PropertyChanged != null)
            {
                foreach (string property in properties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }

        private void ReloadView(object sender, EventArgs e)
        {
            OnPropertyChanged(new[] { "Competitions", });
        }
        #endregion
    }
}
