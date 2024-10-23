using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controller_Tester.Standard_Simulation
{
    /// <summary>
    /// LogWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LogWindow : UserControl
    {
        public LogWindow()
        {
            InitializeComponent();
            
            ShareDataForClass.logWindow_ViewModel = new LogWindow_ViewModel();
            DataContext = ShareDataForClass.logWindow_ViewModel;

            ShareDataForClass.log_window_scrollViewer = FindName("Log_Receive_Window") as ScrollViewer;
            

        }

    }
}
