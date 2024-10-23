using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Tester.Standard_Simulation
{
    public class SerialConnect_Model
    {
        public string controller_SerialPort_Result_Message
        {
            get;
            set;
        }
        public ObservableCollection<string> controllerComPortList
        {
            get;
            set;
        }
        public string select_controllerComPortList
        {
            get;
            set;
        }

        public string serialConnect_Btn_Content
        {
            get;
            set;
        }



    }
}
