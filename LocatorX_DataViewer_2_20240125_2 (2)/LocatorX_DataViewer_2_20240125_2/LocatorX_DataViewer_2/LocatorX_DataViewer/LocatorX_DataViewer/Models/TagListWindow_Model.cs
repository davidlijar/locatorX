using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controller_Tester.Standard_Simulation
{
    public class TagListWindow_Model
    {

        //전송간격
        public ObservableCollection<string> tagList
        {
            get;
            set;
        }
        public string select_TagList
        {
            get;
            set;
        }
        //public int select_TransIntervalList_index
        public int select_TagList_index
        {
            get;
            set;
        }
    }
}
