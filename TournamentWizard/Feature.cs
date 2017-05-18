using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentWizard
{
    public class Feature
    {
        private string _name;
        private object _viewElement;

        public Feature(string name, object viewElement)
        {
            _name = name;
            _viewElement = viewElement;
        }

        public string Name
        {
            get { return _name; }
        }

        public object ViewElement
        {
            get { return _viewElement; }
        }
    }
}
