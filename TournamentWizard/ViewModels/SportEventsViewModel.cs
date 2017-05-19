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
    public class SportEventsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SportEvent> _sportEvents;
        private Action<object> _sportEventLoadCallback;

        public SportEventsViewModel(SportEventContext context, Action<object> sportEventLoadCallback)
        {
            _sportEvents = context.SportEvents.Local;
            _sportEventLoadCallback = sportEventLoadCallback;
        }

        public ObservableCollection<SportEvent> SportEvents
        {
            get { return _sportEvents; }
        }

        #region Commands

        public ICommand AddSportEvent => new CustomCommand(ExecuteAddSportEvent);

        public async void ExecuteAddSportEvent(object parameter)
        {
            var view = new Dialogs.SportEventDialog()
            {
                DataContext = new SportEventDialogViewModel()
            };

            var result = await DialogHost.Show(view, "RootDialog");

            if (result != null)
            {
                SportEvent newSportEvent = result as SportEvent;
                //Debug.Write(newSportEvent.Name);
                //Debug.Write(newSportEvent.FieldCount);
                _sportEvents.Add((SportEvent)result);
            }
        }

        public ICommand DeleteSportEvent => new CustomCommand(ExecuteDeleteSporEvent, CanExecuteDeleteOrLoad);

        public void ExecuteDeleteSporEvent(object parameter)
        {
            var sportEvent = parameter as SportEvent;
            _sportEvents.Remove(sportEvent);
        }

        public bool CanExecuteDeleteOrLoad(object parameter)
        {
            return parameter != null;
        }

        public ICommand LoadSportEvent => new CustomCommand(_sportEventLoadCallback, CanExecuteDeleteOrLoad);

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
