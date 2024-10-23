using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using static Controller_Tester.Standard_Simulation.MainViewModel;

namespace Controller_Tester.Standard_Simulation
{


    public class LogWindow_ViewModel : FrameworkElement, INotifyPropertyChanged
    {
        LogWindow_Model logWindow_Model;


    
        public LogWindow_ViewModel()
        {
            logWindow_Model = new LogWindow_Model();


            LogList = new ObservableCollection<LogData>
            {
                new LogData{ Log = "Hello", IsHighlighted = 1},
                new LogData{ Log = "Hello", IsHighlighted = 2},
                new LogData{ Log = "Hello", IsHighlighted = 3},
            };

            LogList.Clear();

            

        }
        #region  Button Command
        public ICommand ATcommand_Send_Button => new RelayCommand<object>(ATcommand_Send_Button_Run, null);
        private void ATcommand_Send_Button_Run(object x)
        {
            if (ShareDataForClass.controller_serialport.IsOpen == false)
            {
                MessageBox.Show("시리얼포트를 확인해 주세요!");
                return;
            }


            //ShareDataForClass.logWindow_ViewModel.Log_Receive_Message += Log_Send_Message + "\r\n";
            ShareDataForClass.logWindow_ViewModel.LogList.Add(new LogData { Log = Log_Send_Message });
            ShareDataForClass.controller_serialport.Write(ShareDataForClass.logWindow_ViewModel.Log_Receive_Message);

            
        }
        public ICommand Log_Del_Button => new RelayCommand<object>(Log_Del_Button_Run, null);
        private void Log_Del_Button_Run(object x)
        {
            /*
            if (ShareDataForClass.controller_serialport.IsOpen == false)
            {
                MessageBox.Show("시리얼포트를 확인해 주세요!");
                return;
            }
            */
            //Log_Send_Message = "";
            //ShareDataForClass.logWindow_ViewModel.Log_Receive_Message = "";

            ShareDataForClass.logWindow_ViewModel.LogList.Clear();
            //ShareDataForClass.controller_serialport.Write(ShareDataForClass.logWindow_ViewModel.Log_Receive_Message);


        }





        #endregion
        public string Log_Filter
        {
            get;
            set;
        }

        public string Log_Send_Message
        {
            get;
            set;
        }

        public ObservableCollection<LogData> _logList;
        public ObservableCollection<LogData> LogList
        {
            get => _logList;
            set
            {
                if (_logList != value)
                {
                    _logList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Log { get; set; }

        public string Log_Receive_Message
        {
            get { return logWindow_Model.log_Message; }
            set
            {
                
                logWindow_Model.log_Message = value;

                DispatcherService.Invoke((System.Action)(() =>
                {
                    ShareDataForClass.log_window_scrollViewer.ScrollToEnd(); //자동 스크롤
                }));



                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            if (!String.IsNullOrEmpty(name))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }


}
