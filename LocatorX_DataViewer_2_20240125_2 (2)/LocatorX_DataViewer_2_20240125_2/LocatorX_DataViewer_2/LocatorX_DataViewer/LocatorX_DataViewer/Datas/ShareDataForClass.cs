
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controller_Tester.Standard_Simulation
{
    static class ShareDataForClass
    {
        static public string COMMAND_1 = "1. 전체 온도조절기 상태 응답 (표준준수)";
        static public string COMMAND_2 = "2. 상세 상태 조회 (개별 조회 만 가능)";
        static public string COMMAND_3 = "3. 온도조절기 ON/OFF 제어 요구";
        static public string COMMAND_4 = "4. 온도조절기 온도 설정 제어 요구";
        static public string COMMAND_5 = "5. 온도조절기 외출 제어 요구";
        static public string COMMAND_6 = "[x]6. 상세 상태 제어 (개별 제어 만 가능) 요구";
        static public string COMMAND_7 = "6. 온도조절기 구간 난방 설정 및 난방 On/Off 여부 송신";
        static public string COMMAND_8 = "8. 환수 온도 조회 요구";
        static public string COMMAND_9 = "9. [Bypass] 밸브 조절기 최종 상태 요청에 대한 데이터 전달";
        static public string COMMAND_10 = "10. [Bypass] 통합 스위치에서 밸브 열림 제어 전달";
        static public string COMMAND_11 = "[x]11. [Bypass] 존 설정 값 전달 (초기 세팅 시 1회 발생)";
        static public string COMMAND_12 = "11. 특성 조회 요구 (표준 준수)";

        static public string COMMAND9_ROOM_CNT_1 = "1";
        static public string COMMAND9_ROOM_CNT_2 = "2";
        static public string COMMAND9_ROOM_CNT_3 = "3";
        static public string COMMAND9_ROOM_CNT_4 = "4";
        static public string COMMAND9_ROOM_CNT_5 = "5";
        static public string COMMAND9_ROOM_CNT_6 = "6";
        static public string COMMAND9_ROOM_CNT_7 = "7";
        static public string COMMAND9_ROOM_CNT_8 = "8";


        static public MainViewModel mainViewModel;
        static public LogWindow_ViewModel logWindow_ViewModel;
        static public _3DViewerWindow_ViewModel __3DViewerWindow_ViewModel;

        static public TagListWindow_ViewModel TagListWindow_ViewModel;
        static public ScrollViewer log_window_scrollViewer;
        static public SerialConnect_ViewModel serialConnect_ViewModel;


        static public TagViewer tagViewer;
        static public ObservableCollection<TagViewer_ViewModel> tagViewer_ViewModels;

        static public Tag locator;
        static public ObservableCollection<Tag> ble_tag;


        static public SerialPort controller_serialport;
    }
}
