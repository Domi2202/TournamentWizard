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

namespace TournamentWizard.ViewModels
{
    public class SportEventsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SportEvent> _sportEvents;

        public SportEventsViewModel(SportEventContext context)
        {
            _sportEvents = context.SportEvents.Local;
        }

        public ObservableCollection<SportEvent> SportEvents
        {
            get { return _sportEvents; }
        }

        #region Commands

        public ICommand AddSportEvent => new CustomCommand(ExecuteAddSportEvent);

        public async void ExecuteAddSportEvent(object parameter)
        {
            var view = new Dialogs.SportEventDialog();

            var result = await DialogHost.Show(view, "RootDialog");

            //_sportEvents.Add(new SportEvent()
            //{
            //    Name = "SportEvent",
            //    FieldCount = 3
            //});
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
