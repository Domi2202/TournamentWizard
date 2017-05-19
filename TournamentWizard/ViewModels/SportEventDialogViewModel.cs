using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TournamentWizard.Models;
using TournamentWizard.Commands;
using MaterialDesignThemes.Wpf;

namespace TournamentWizard.ViewModels
{
    class SportEventDialogViewModel : INotifyPropertyChanged
    {
        private string _name;
        private int _fieldCount;
        private SportEvent _newSportEvent;

        public SportEventDialogViewModel()
        {
            _fieldCount = 3;
            _newSportEvent = new SportEvent()
            {
                FieldCount = _fieldCount
            };
        }

        public string Name
        {
            get { return _name; }
            set
            {
                this.MutateVerbose(ref _name, value, RaisePropertyChanged());
                _newSportEvent.Name = _name;
            }
        }

        public int FieldCount
        {
            get { return _fieldCount; }
            set
            {
                this.MutateVerbose(ref _fieldCount, value, RaisePropertyChanged());
                _newSportEvent.FieldCount = _fieldCount;
            }
        }

        public SportEvent NewSportEvent
        {
            get { return _newSportEvent; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
