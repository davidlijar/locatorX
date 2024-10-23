using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using System;
using System.Reflection;
using System.IO;
using System.Windows.Threading;
using System.Windows;
using System.Diagnostics;
using System.Text;

namespace Controller_Tester.Standard_Simulation
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {

            ShareDataForClass.mainViewModel = this;

   
        }

        #region Binding 변수
        private bool _gateCtrlPannel_Expended;
        public bool GateCtrlPannel_Expended {
            get 
            {
                return _gateCtrlPannel_Expended;
            }
            set
            {
                _gateCtrlPannel_Expended = value;
                OnPropertyChanged("GateCtrlPannel_Expended");
            }
            
        }
        private bool _LEDcontrollerCtrlPannel_Expended;
        public bool LEDcontrollerCtrlPannel_Expended
        {
            get
            {
                return _LEDcontrollerCtrlPannel_Expended;
            }
            set
            {
                _LEDcontrollerCtrlPannel_Expended = value;
                OnPropertyChanged("LEDcontrollerCtrlPannel_Expended");
            }

        }
        private bool _RGBsensorCtrlPannel_Expended;
        public bool RGBsensorCtrlPannel_Expended
        {
            get
            {
                return _RGBsensorCtrlPannel_Expended;
            }
            set
            {
                _RGBsensorCtrlPannel_Expended = value;
                OnPropertyChanged("RGBsensorCtrlPannel_Expended");
            }

        }
        private int _currentProgress;
        public int MainDownloadProgress
        {
            get
            {
                return _currentProgress;
            }
            set
            {
                _currentProgress = value;
                OnPropertyChanged("RGBsensorCtrlPannel_Expended");
            }
        }

        #endregion

        public static class DispatcherService
        {
            /* UI 업데이트시 문제 해결을 위해 사용됨.
             * Exception 내용 : 이 형식의 CollectionView에서는 발송자 스레드와 다른 스레드에서의 해당 SourceCollection에 대한 변경 내용을 지원하지 않습니다.
             */
            public static void Invoke(Action action)
            {
                Dispatcher dispatchObject = Application.Current != null ? Application.Current.Dispatcher : null;
                if (dispatchObject == null || dispatchObject.CheckAccess())
                    action();
                else
                    dispatchObject.Invoke(action);
            }
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
