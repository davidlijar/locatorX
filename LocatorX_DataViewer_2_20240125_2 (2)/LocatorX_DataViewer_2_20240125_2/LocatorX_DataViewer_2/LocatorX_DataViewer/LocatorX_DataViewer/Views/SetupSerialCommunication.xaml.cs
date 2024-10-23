using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Controller_Tester.Standard_Simulation
{
    /// <summary>
    /// SetupSerialCommunication.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SetupSerialCommunication : UserControl
    {
        private HwndSource _hwndSource = null;
        public SetupSerialCommunication()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);

            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShareDataForClass.serialConnect_ViewModel = new SerialConnect_ViewModel();

            Thread.Sleep(1000);
            this.DataContext = ShareDataForClass.serialConnect_ViewModel;


        }







    }
}
