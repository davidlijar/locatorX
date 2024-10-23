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
    /// PacketWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class _3DViewerWindow : UserControl
    {
        public _3DViewerWindow()
        {
            InitializeComponent();


            ShareDataForClass.__3DViewerWindow_ViewModel = new _3DViewerWindow_ViewModel();
            DataContext = ShareDataForClass.__3DViewerWindow_ViewModel;

#if false
            this.MouseMove += (s, e) => ShareDataForClass._3DViewerWindow_ViewModel.OnMouseMove(e.GetPosition(this));
            this.MouseDown += (s, e) => { if (e.LeftButton == MouseButtonState.Pressed) ShareDataForClass._3DViewerWindow_ViewModel.OnMouseDown(e.GetPosition(this)); };
            this.MouseUp += (s, e) => ShareDataForClass._3DViewerWindow_ViewModel.OnMouseUp();
#endif
            //this.MouseDown += new MouseButtonEventHandler(ShareDataForClass._3DViewerWindow_ViewModel.Viewport3D_MouseDown);


        }



    }
}
