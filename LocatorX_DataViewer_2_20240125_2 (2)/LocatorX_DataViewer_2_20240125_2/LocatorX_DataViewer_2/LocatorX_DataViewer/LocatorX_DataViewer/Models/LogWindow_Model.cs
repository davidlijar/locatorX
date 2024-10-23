using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controller_Tester.Standard_Simulation
{
    public class LogWindow_Model
    {



        public ScrollViewer log_Receive_Message_Name
        {
            get;
            set;
        }

        public string log_Message
        {
            get;
            set;
        }
        public string log_Send_Message
        {
            get;
            set;
        }
        //Btn_ATcommand_Send
    }
}
