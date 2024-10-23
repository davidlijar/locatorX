using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
          
namespace Controller_Tester.Standard_Simulation
{
    public class Connector : DiagramObject
    {
        //The Connector's X and Y properties are always 0 because the line coordinates are actually determined
        //by the Start.X, Start.Y and End.X, End.Y Tags' properties.
        public override double X
        {
            get { return 0; }
            set { }
        }

        public override double Y
        {
            get { return 0; }
            set { }
        }

        private Tag _start;
        public Tag Start
        {
            get { return _start; }
            set
            {
                _start = value;
                OnPropertyChanged("Start");
            }
        }

        private Tag _end;
        public Tag End
        {
            get { return _end; }
            set
            {
                _end = value;
                OnPropertyChanged("End");
            }
        }
    }
}
