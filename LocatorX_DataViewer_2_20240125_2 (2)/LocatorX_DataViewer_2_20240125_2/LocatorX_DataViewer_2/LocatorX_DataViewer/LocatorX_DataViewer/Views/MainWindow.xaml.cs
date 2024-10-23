using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Media;
using System.IO;
using System.Threading;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Controller_Tester.Standard_Simulation
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            this.Title = "LocationX Locator Tester:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();



            ShareDataForClass.mainViewModel = new MainViewModel();

            DataContext = ShareDataForClass.mainViewModel;



        }


        void Window_Close(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }
        public IntPtr WndProc(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            UInt32 WM_DEVICECHANGE = 0x0219;
            //UInt32 DBT_DEVTUP_VOLUME = 0x02; //저장장치
            //UInt32 DBT_DEVTUP_VOLUME = 0x03; //USB to Serial
            UInt32 DBT_DEVICEARRIVAL = 0x8000;
            UInt32 DBT_DEVICEREMOVECOMPLETE = 0x8004;

            //디바이스 연결시
            if ((nMsg == WM_DEVICECHANGE) && (wParam.ToInt32() == DBT_DEVICEARRIVAL))
            {
                ShareDataForClass.serialConnect_ViewModel.Find_port();
            }

            //디바이스 연결 해제시...
            if ((nMsg == WM_DEVICECHANGE) && (wParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE))
            {
                ShareDataForClass.serialConnect_ViewModel.Find_port();
            }

            return IntPtr.Zero;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
