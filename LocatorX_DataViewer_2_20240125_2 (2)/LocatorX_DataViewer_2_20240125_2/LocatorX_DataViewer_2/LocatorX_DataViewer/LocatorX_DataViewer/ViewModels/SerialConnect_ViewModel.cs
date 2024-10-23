using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static Controller_Tester.Standard_Simulation.MainViewModel;

namespace Controller_Tester.Standard_Simulation
{



    public class SerialConnect_ViewModel : INotifyPropertyChanged
    {

        #region 전역변수 정의
        int timer_call_cnt = 0;
        DispatcherTimer GroupAcker_monitor_timer;

        public LinkedList<string> LogWindowLine;
        SerialConnect_Model deviceConnect = new SerialConnect_Model();

        static public bool gateway_connection_state = false;
        //string gateway_recieved_data = "";
        public String receiveTextParse = "";


        public System.Windows.Threading.DispatcherTimer SendCommand_timer;


        public int Curr_Command_Index = 0;

        public double tagDistence_x = 0;
        public double tagDistence_y = 0;


        #endregion

        #region 생성자 함수
        public SerialConnect_ViewModel()
        {

            Serial_port_init();

            SerialConnect_Btn_Content = "Connect";

            SendCommand_timer = new DispatcherTimer();
            SendCommand_timer.Tick += SendData_count_tick;

            LogWindowLine = new LinkedList<string>();



        }
        #endregion

        public void SendData_Count_timer_init(int index)
        {

            SendCommand_timer.Stop();

            if (index == 0)
            {
                return;
            }
            int interval_time = 0;

            if(index ==1)
            {
                interval_time = 100;
            }
            else if(index == 2)
            {
                interval_time = 300;
            }
            else if(index == 3)
            {
                interval_time = 500;
            }
            else if(index == 4)
            {
                interval_time = 800;
            }
            else if (index == 5)
            {
                interval_time = 1000;
            }
            else
            {
                ;
            }

            //호출 함수 설정


            //함수 호출 주기 설정
            SendCommand_timer.Interval = TimeSpan.FromMilliseconds(interval_time) ;

            //타이머 시작
            SendCommand_timer.Start();
        }
        public  void SendCommand_Timer_Stop()
        {
            SendCommand_timer.Stop();
        }
        int packet_index = 0;
        void SendData_count_tick(object sender, EventArgs e)
        {


        }

        public void Serial_port_init()
        {
            /*COM 포트를 초기화한다.*/

            if (ShareDataForClass.controller_serialport != null && ShareDataForClass.controller_serialport.IsOpen)
            {
                ShareDataForClass.controller_serialport.Close();
            }
            if (ShareDataForClass.controller_serialport != null && ShareDataForClass.controller_serialport.IsOpen)
            {
                ShareDataForClass.controller_serialport.Close();
            }


            //ShareDataForClass.controller_serialport = new SerialPort("COM17", 921600, Parity.None, 8, StopBits.One);
            ShareDataForClass.controller_serialport = new SerialPort("COM17", 115200, Parity.None, 8, StopBits.One);
            ShareDataForClass.controller_serialport.ReadBufferSize = 100;// 4096 + 4096;
            ShareDataForClass.controller_serialport.WriteBufferSize = 100;// 4096 + 4096;
            //ShareDataForClass.controller_serialport.Encoding = Encoding.GetEncoding("ISO-8859-1");

            /*포트를 초기화한다.*/
            //ShareDataForClass.string_TB_Printer_COM = "COM1";
            deviceConnect.controllerComPortList = new ObservableCollection<string>();
            deviceConnect.controllerComPortList.Add("COM17");

            deviceConnect.controller_SerialPort_Result_Message = "Check the device connection";

            /*COM 포트를 찾는다*/
            Find_port();

            //타이머 초기화
            GroupAcker_monitor_timer = new DispatcherTimer();

        }


        public void Find_port()
        {

            deviceConnect.controllerComPortList.Clear();

            //class System.IO.Ports.SerialPort를 이용하여
            //portsArray에 현재 접속되어 있는 포트 이름들을 모두 적재합니다.
            string[] portsArray = SerialPort.GetPortNames();

            //배열에 적재된 포트이름들을 콤보박스의 아이템으로추가시킵니다.
            foreach (string portnumber in portsArray)
            {
                deviceConnect.controllerComPortList.Add(portnumber);
            }

        }
        //콤보박스 연결

        public ObservableCollection<string> ControllerPortList
        {
            get { return deviceConnect.controllerComPortList; }
            set
            {
                deviceConnect.controllerComPortList = value;
                NotifyPropertyChanged();
            }
        }
        public string SerialConnect_Btn_Content
        {
            get { return deviceConnect.serialConnect_Btn_Content; }
            set
            {
                deviceConnect.serialConnect_Btn_Content = value;
                NotifyPropertyChanged();
            }
        }
        public string Controller_SerialPort_Result_Message
        {
            get { return deviceConnect.controller_SerialPort_Result_Message; }
            set
            {
                deviceConnect.controller_SerialPort_Result_Message = value;
                NotifyPropertyChanged();
            }
        }
        public string Select_controllerComPortList
        {
            get { return deviceConnect.select_controllerComPortList; }
            set
            {
                deviceConnect.select_controllerComPortList = value;
                NotifyPropertyChanged();
            }
        }


        #region COMMAND 정의

        /*
        1. 전체 온도조절기 상태 조회 (표준 준수)
        2. 상세 상태 조회 (개별 조회 만 가능)
        3. Bypass 조회
        4. 온도조절기 ON/OFF 제어 요구
        5. 온도조절기 온도 설정 제어 요구
        7. 온도조절기 외출 제어 요구
        9. 상세 상태 제어 (개별 제어 만 가능) 요구
        10. 온도조절기 구간 난방 설정 및 난방 On/Off 여부 송신
        11. 환수 온도 조회 요구
        12. 특성 조회 요구 (표준 준수)
 
         */

        public byte getHex(string srcValue)
        {
            //srcValue = "78";

            return Convert.ToByte(srcValue, 16);

            //리턴되는 값은 0x78
        }



#endregion

        public void Command_send_with_serial(byte[] arr, int length)
        {
            if (ShareDataForClass.controller_serialport.IsOpen == true)
            {

                try
                {
                    ShareDataForClass.controller_serialport.Write(arr, 0, length);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

            }
        }






        public ICommand Controller_Connection_Button => new RelayCommand<object>(Controller_Connection_Button_Run, null);

        private void Controller_Connection_Button_Run(object x)
        {
            Controller_Connection();
        }

        public void Controller_Connection()
        {
            //if (cb_spectrum_analyzer_serialPort.Text == "")
            if (Select_controllerComPortList == "")
            {
                MessageBox.Show("Com Port를 확인해 주세요!");
                return;
            }

            gateway_connection_state = false;



            if (SerialConnect_Btn_Content.Equals("Disconnect"))
            {
                //포트가 열려있다면 일단 닫습니다.
                if (ShareDataForClass.controller_serialport.IsOpen == true)
                {

                    //포트가 열려있다면 일단 닫습니다.
                    ShareDataForClass.controller_serialport.DataReceived -= Locator_DataReceviedHandler;
                    ShareDataForClass.controller_serialport.Close();
                }

                ShareDataForClass.serialConnect_ViewModel.SendCommand_Timer_Stop();

                SerialConnect_Btn_Content = "Connect";
                Controller_SerialPort_Result_Message = ShareDataForClass.controller_serialport.PortName + "is Disconnected.\n";
                timer_call_cnt = 0;
                return;
            }

            //데이터를 수신받을때 사용하는 핸들러 함수를 등록한다.
            ShareDataForClass.controller_serialport.DataReceived += new SerialDataReceivedEventHandler(Locator_DataReceviedHandler);

            if (!String.IsNullOrEmpty(Select_controllerComPortList))
            {
                //콤보박스에 선택되어있는 아이템을 포트명으로 가져옵니다.
                ShareDataForClass.controller_serialport.PortName = Select_controllerComPortList;// cb_spectrum_analyzer_serialPort.SelectedItem.ToString();
                //main_form.serial_portname = comboBox.SelectedItem.ToString(); 
                //보레이트를 설정합니다.
                //원래 패리티비트, 스탑비트 설정 등도 있으나 생략합니다.
                ShareDataForClass.controller_serialport.BaudRate = 115200;// 921600;
                ShareDataForClass.controller_serialport.DataBits = 8;
                ShareDataForClass.controller_serialport.StopBits = StopBits.One;
                ShareDataForClass.controller_serialport.Parity = Parity.None;
                ShareDataForClass.controller_serialport.ReceivedBytesThreshold = 1;

                ShareDataForClass.controller_serialport.ReadTimeout = 500;
                ShareDataForClass.controller_serialport.WriteTimeout = 500;

                //ShareDataForClass.controller_serialport.NewLine = "\n";
                //포트를 엽니다.
                try
                {
                    ShareDataForClass.controller_serialport.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

                SerialConnect_Btn_Content = "Disconnect";
                Controller_SerialPort_Result_Message = ShareDataForClass.controller_serialport.PortName + " Connected.";


            }
            else
            {
                Controller_SerialPort_Result_Message = "Select Port.";
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
#region LOG 프린터




        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        public void Log_Window_Print(string log, int TxRx_msg_type)
        {
            DispatcherService.Invoke((System.Action)(() =>
            { 
                try
                {

                    EndDate = DateTime.Now;
                    TimeSpan dateDiff = EndDate - StartDate;

                    StartDate = EndDate;
    ;
                    string log_data_line = "[" + dateDiff.Milliseconds.ToString("D3") + "] " + log;

                    //string log_data_line = "[" + DateTime.Now.ToString("hh:mm:ss.ffff") + "] " + log + "\r\n"

                    int isTx = TxRx_msg_type;


                    ShareDataForClass.logWindow_ViewModel.LogList.Add(new LogData { Log = log_data_line, IsHighlighted = isTx });
                    if (ShareDataForClass.logWindow_ViewModel.LogList.Count > 50)
                    {
                        ShareDataForClass.logWindow_ViewModel.LogList.RemoveAt(0);
                    }

                    ShareDataForClass.log_window_scrollViewer.ScrollToEnd(); //자동 스크롤
           


                }
                catch (Exception e)
                {

                }
             }));
        }


#endregion

#region 컨트롤러로 부터 데이터 수신
    
  
        

        public void Serial_Data_Process(string receiveBuffer)
        {

            /*로그 처리*/


            string[] receiveBuffer_data = receiveBuffer.Split('-');
            //로그 출력
            //ShareDataForClass.logWindow_ViewModel.Log_Receive_Message = receiveTextParse;

            //커맨드 확인
            string Cmd = receiveBuffer_data[3];

            if (Cmd.Equals("91"))
            {
                Log_Window_Print(receiveBuffer, 2);//수신 
            }
            else
            {
                Log_Window_Print(receiveBuffer, 3);//수신 
            }
                

        }
        private List<byte> receivedData = new List<byte>();
        private bool isReceiving = false;
        private int receivedDataLength = 0;


        private void Locator_DataReceviedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                try
                {
              
                    string data = ShareDataForClass.controller_serialport.ReadLine();

                    // 문자열을 콤마를 기준으로 분리합니다.
                    string[] parts = data.Split(',');



                    if (ShareDataForClass.TagListWindow_ViewModel.Check_TagID_inTagList(parts[1]) == false)
                    {
                        DispatcherService.Invoke((System.Action)(() =>
                        {
                            ShareDataForClass.TagListWindow_ViewModel.Add_TagList(parts[1]);
                        }));

                    }
                    if (ShareDataForClass.TagListWindow_ViewModel.Select_TagList != parts[1])
                    {
                        return;
                    }
                    else
                    {
                        Log_Window_Print(data, 2);
                        Console.WriteLine("TagID :" + parts[0]);
                        Console.WriteLine("Mac :" + parts[1]);
                        Console.WriteLine("Azimuth :" + parts[2]);
                        Console.WriteLine("Elevation :" + parts[3]);

                        /*위치 계산*/
                        double doubleAzimuth = double.Parse(parts[2]);
                        double doubleElevation = double.Parse(parts[3]);

                        double degrees = doubleElevation;
                        double radians = degrees * (3.14 / 180.0);  // 30도를 라디안으로 변환
                        double tan_value = Math.Tan(radians);            // 탄젠트 값 계산
                        //tagDistence_x = ((1 / tan_value)*100) + 500;
                        tagDistence_x = (1 / tan_value)*100;
                        //회전 후 좌표
                        double tagX = tagDistence_x * Math.Cos(doubleAzimuth * (3.14 / 180.0))+500;
                        double tagY = tagDistence_x * Math.Sin(doubleAzimuth * (3.14 / 180.0))+500;
                        

                        
                        //ShareDataForClass.tagViewer_ViewModels[0].Add_ble_tag(0, parts[1], tagDistence_x, tagDistence_y);
                        //ShareDataForClass.tagViewer_ViewModels[0].set_ble_position(parts[1], tagDistence_x, tagDistence_y);
                        ShareDataForClass.tagViewer_ViewModels[0].Add_ble_tag(0, parts[1], tagX, tagY);
                        ShareDataForClass.tagViewer_ViewModels[0].set_ble_position(parts[1], tagX, tagY);


                        ShareDataForClass.__3DViewerWindow_ViewModel.TagID = parts[1];// int.Parse(parts[2]); 
                        ShareDataForClass.__3DViewerWindow_ViewModel.Azimuth = parts[2];// int.Parse(parts[2]); 
                        ShareDataForClass.__3DViewerWindow_ViewModel.Elevation = parts[3];
                        ShareDataForClass.__3DViewerWindow_ViewModel.Rotate3DObject(double.Parse(ShareDataForClass.__3DViewerWindow_ViewModel.Azimuth), double.Parse(ShareDataForClass.__3DViewerWindow_ViewModel.Elevation));


                        // 분리된 각 부분을 처리합니다.
                        foreach (var part in parts)
                        {
                            Console.WriteLine(part);

                            // 여기에 각 부분을 처리하는 추가 로직을 구현할 수 있습니다.
                            // 예: part 값을 기반으로 하는 데이터 처리나 변환 로직
                        }
                    }
               
                }
                catch (System.IO.IOException ex)
                {
                    // IOException이 발생했을 때 실행할 코드를 작성합니다.
                    // 예를 들어, 로그를 남기거나 사용자에게 오류 메시지를 표시할 수 있습니다.
                    Console.WriteLine("I/O 작업 중 오류가 발생했습니다: " + ex.Message);
                }
            }));



        }




        #endregion  //게이트웨이로 부터 수신
        #region 화면갱신
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            if (!String.IsNullOrEmpty(name))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
#endregion
    }
}
