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
    public class EliminationFinalBoxViewModel : INotifyPropertyChanged
    {
        private EliminationFinal _elimnationFinal;

        #region bindingProperties
        /// <summary>
        /// Gets or sets the associated EliminationFinal
        /// </summary>
        public EliminationFinal EliminationFinal
        {
            get { return _elimnationFinal; }
            set { _elimnationFinal = value; }
        }
        #endregion

        #region propertyChanges
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
