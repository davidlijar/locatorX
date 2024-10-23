using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Controller_Tester.Standard_Simulation
{



    public class TagViewer_ViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Tag> _tags;
        public ObservableCollection<Tag> Tags
        {
            get { return _tags ?? (_tags = new ObservableCollection<Tag>()); }
        }

        private ObservableCollection<Connector> _connectors;
        public ObservableCollection<Connector> Connectors
        {
            get { return _connectors ?? (_connectors = new ObservableCollection<Connector>()); }
        }



        private DispatcherTimer tagUpdateTimer;

        private DiagramObject _selectedObject;
        public DiagramObject SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                Tags.ToList().ForEach(x => x.IsHighlighted = false);

                _selectedObject = value;
                if (_selectedObject == null)
                {
                    Debug.WriteLine("error - _selectedObject is nell");
                    return;
                }
                DiagramObject _selectedObject_data = _selectedObject;
                if (_selectedObject_data == null)
                {
                    Debug.WriteLine("error - _selectedObject_data is nell");
                    return;
                }
                if (_selectedObject_data.Name == null)
                {
                    Debug.WriteLine("error - _selectedObject_data.Name is nell");
                    return;
                }
                OnPropertyChanged("Start");

                DeleteCommand.IsEnabled = value != null;

                var connector = value as Connector;
                if (connector != null)
                {
                    if (connector.Start != null)
                        connector.Start.IsHighlighted = true;

                    if (connector.End != null)
                        connector.End.IsHighlighted = true;
                }





            }
        }
        public String _pair_group_name = "";
        public void Set_pair_group_name(String group_name)
        {
            _pair_group_name = group_name;
        }
        public String Get_pair_group_name()
        {
            return _pair_group_name;
        }
        Tag newTag1;
        Tag newTag2;

        public TagViewer_ViewModel()
        {

            ShowAllCoordinates = true;
            ShowNames = true;

            ShareDataForClass.locator = new Tag();
            

            ShareDataForClass.ble_tag = new ObservableCollection<Tag>();

            newTag1 = new Tag();
            newTag2 = new Tag();

            newTag1.TagType = (int)Tag.TagTypeEnum.LOCATROR;
            newTag1.Mac_Id = "11:22:33:44:55:66";
            newTag1.Rssi = -40;
            newTag1.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/locationX_locator_img.png", UriKind.RelativeOrAbsolute));
            newTag1.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/locationX_locator_img.png", UriKind.RelativeOrAbsolute));
            newTag1.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/locationX_locator_img.png", UriKind.RelativeOrAbsolute));

            newTag1.Name = "LOCATOR";
            newTag1.X =500;
            newTag1.Y = 500;
            Tags.Add(newTag1);
            InitializeTagUpdateTimer();

#if false
            newTag2.TagType = (int)Tag.TagTypeEnum.BLE_TAG;
            newTag2.Mac_Id = "11:22:33:44:55:66";
            newTag2.Rssi = -40;
            newTag2.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/rgb_sensor_white.png", UriKind.RelativeOrAbsolute));
            newTag2.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/rgb_sensor_white.png", UriKind.RelativeOrAbsolute));
            newTag2.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/rgb_sensor_white.png", UriKind.RelativeOrAbsolute));

            newTag2.Name = "Tag21";
            newTag2.X = 400;
            newTag2.Y = 200;
            Tags.Add(newTag2);


            var connector1 = new Connector();
            connector1.Start = newTag1;
            connector1.End = newTag2;
            Connectors.Add(connector1);
#endif

#if false
            //렌덤하게 초기화
            _tags = new ObservableCollection<Tag>(TagsDataSource.GetRandomTags());
            _connectors = new ObservableCollection<Connector>(TagsDataSource.GetRandomConnectors(Tags.ToList()));

            var newTag1 = new Tag();

            newTag1.TagType = (int)Tag.TagTypeEnum.LOCATROR;
            newTag1.Mac_Id = "11:22:33:44:55:66";
            newTag1.Rssi = -40;
            newTag1.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_white.png", UriKind.RelativeOrAbsolute));
            newTag1.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_red.png", UriKind.RelativeOrAbsolute));
            newTag1.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_green.png", UriKind.RelativeOrAbsolute));

            newTag1.Name = "Tag";
            newTag1.X = 200;
            newTag1.Y = 200;
            Tags.Add(newTag1);

            var newTag2 = new Tag();
            newTag2.TagType = (int)Tag.TagTypeEnum.BLE_TAG;
            newTag2.Mac_Id = "AA:BB:CC:DD:EE:DD";
            newTag2.Rssi = -60;
            newTag2.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/rgb_sensor_white.png", UriKind.RelativeOrAbsolute));
            newTag2.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/rgb_sensor_red.png", UriKind.RelativeOrAbsolute));
            newTag2.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/rgb_sensor_green.png", UriKind.RelativeOrAbsolute));
            newTag2.Name = "BLE_TAG";
            newTag2.X = 400;
            newTag2.Y = 500;
            Tags.Add(newTag2);

            var newTag3 = new Tag();
            newTag3.TagType = (int)Tag.TagTypeEnum.BLE_TAG;
            newTag3.Mac_Id = "AA:AA:AA:AA:AA:AA";
            newTag3.Rssi = -70;
            newTag3.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/usb_gateway_white.png", UriKind.RelativeOrAbsolute));
            newTag3.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/usb_gateway_red.png", UriKind.RelativeOrAbsolute));
            newTag3.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/usb_gateway_green.png", UriKind.RelativeOrAbsolute));
            newTag3.Name = "BLE_TAG";
            newTag3.X = 600;
            newTag3.Y = 200;
            Tags.Add(newTag3);

            var newTag4 = new Tag();
            newTag4.TagType = (int)Tag.TagTypeEnum.BLE_TAG;
            newTag4.Mac_Id = "AA:22:33:44:55:66";
            newTag4.Rssi = -40;
            newTag4.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_white.png", UriKind.RelativeOrAbsolute));
            newTag4.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_red.png", UriKind.RelativeOrAbsolute));
            newTag4.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_green.png", UriKind.RelativeOrAbsolute));
            newTag4.Name = "BLE_TAG";
            newTag4.X = 500;
            newTag4.Y = 200;
            Tags.Add(newTag4);


            var connector1 = new Connector();
            connector1.Start = newTag2;
            connector1.End = newTag3;
            Connectors.Add(connector1);

            var connector2 = new Connector();
            connector2.Start = newTag2;
            connector2.End = newTag1;
            Connectors.Add(connector2);

            var connector3 = new Connector();
            connector3.Start = newTag2;
            connector3.End = newTag4;
            Connectors.Add(connector3);
            
#endif

        }


        private void InitializeTagUpdateTimer()
        {
            tagUpdateTimer = new DispatcherTimer();
            tagUpdateTimer.Interval = TimeSpan.FromSeconds(1); // 타이머가 1초마다 트리거됩니다.
            tagUpdateTimer.Tick += Timer_Tick;

            tagUpdateTimer.Start();
        }

        int Tick_Cnt = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            Tick_Cnt++;

#if false

            newTag2.X = newTag2.X + 10;
            newTag2.Y = newTag2.X + 10;

            if(Tick_Cnt == 3)
            {
                Add_ble_tag(0,"AA:BB:CC:DD:E1",300,200);
            }
            if (Tick_Cnt == 6)
            {
                Add_ble_tag(0, "AA:BB:CC:DD:E2", 300, 400);
            }
            if (Tick_Cnt == 7)
            {
                Add_ble_tag(0, "AA:BB:CC:DD:E2", 300, 400);
            }

            if (Tick_Cnt == 9)
            {
                Delete_ble_tag(0, "AA:BB:CC:DD:E1");
                
            }
            if (Tick_Cnt == 12)
            {
                Delete_ble_tag(0, "AA:BB:CC:DD:E2");
                Tick_Cnt = 0;
            }
#endif
        }


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

        public void Add_locator(double x, double y)
        {
            Debug.WriteLine("CHECK *************************************************************");
            Debug.WriteLine("CHECK Add_locator");
            Debug.WriteLine("CHECK *************************************************************");
            DispatcherService.Invoke((System.Action)(() =>
            {
                ShareDataForClass.locator.TagType = (int)Tag.TagTypeEnum.LOCATROR;
                ShareDataForClass.locator.Mac_Id = "";
                ShareDataForClass.locator.Rssi = 0;
                ShareDataForClass.locator.TagGroup = 0;
                ShareDataForClass.locator.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/usb_gateway_white.png", UriKind.RelativeOrAbsolute));
                ShareDataForClass.locator.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/usb_gateway_red.png", UriKind.RelativeOrAbsolute));
                ShareDataForClass.locator.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/usb_gateway_green.png", UriKind.RelativeOrAbsolute));
                ShareDataForClass.locator.Name = "LOCATOR";
                ShareDataForClass.locator.X = x;
                ShareDataForClass.locator.Y = y;
                Tags.Add(ShareDataForClass.locator);
            }));
        }
        public void Delete_gateway()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                Tags.Remove(ShareDataForClass.locator);
            }));
        }

        public void set_ble_position(string Mac, double x, double y)
        {
            foreach (Tag tag in Tags)
            {
                if (tag.Mac_Id.Equals(Mac))
                {
                    tag.X = x;
                    tag.Y = y;
                    break;
                }
            }
        }
        public void Add_ble_tag(int group_id, String Mac, double x, double y)
        {
            /*Tag 존재 확인*/
            bool isTagPresent = false;
            foreach (Tag tag in Tags)
            {
                if (tag.Mac_Id.Equals(Mac))
                {
                    isTagPresent = true;
                    break;
                }
            }
            if(isTagPresent == true)
            {
                return;
            }





            DispatcherService.Invoke((System.Action)(() =>
            {
                Tag tag = new Tag();
                tag.TagType = (int)Tag.TagTypeEnum.BLE_TAG;
                tag.Mac_Id = Mac;
 

                tag.TagGroup = group_id;
                tag.TagState_Normal = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_white.png", UriKind.RelativeOrAbsolute));
                tag.TagState_Select = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_red.png", UriKind.RelativeOrAbsolute));
                tag.TagState_Move = new BitmapImage(new Uri("pack://application:,,,/Resources/main_led_green.png", UriKind.RelativeOrAbsolute));
                tag.Name = "BLE_TAG";

                tag.X = x;
                tag.Y = y;

                Tags.Add(tag);


                //그룹리스트 업데이트
                int tag_count = Tags.Count;
                int tag_group = group_id;
                Debug.WriteLine("<BLE TAG>");
                Debug.WriteLine("tag_group :" + tag_group);
                Debug.WriteLine("tag_count :" + tag_count);


            }));
        }

        public void Delete_ble_tag(int group_id, String TagMac)
        {

            Random random = new Random();

            DispatcherService.Invoke((System.Action)(() =>
            {


                foreach (Tag tag in Tags)
                {
                    if (tag.Mac_Id.Equals(TagMac))
                    {
                        Tags.Remove(tag);
                        break;
                    }
                }


            }));
        }

        public void Add_connect(Tag locator, Tag tag_adv)
        {
            /*게이트웨이와 연결된 상태*/
            bool isTagGateway = false;
            if (locator.Mac_Id.Equals(ShareDataForClass.locator.Mac_Id))
            {
                isTagGateway = true;
            }


            DispatcherService.Invoke((System.Action)(() =>
            {

                bool check_connect_exist = false;
                foreach (Connector item in Connectors)
                {   //경우 1
                    if (item.Start.Mac_Id.Equals(locator.Mac_Id))
                    {
                        foreach (Connector item2 in Connectors)
                        {
                            if (item2.End.Mac_Id.Equals(tag_adv.Mac_Id))
                            {
                                Debug.WriteLine("Start - End 중복됨");
                                check_connect_exist = true;
                            }
                        }
                    }
                    //경우 2
                    else if (item.End.Mac_Id.Equals(locator.Mac_Id))
                    {
                        foreach (Connector item2 in Connectors)
                        {
                            if (item2.Start.Mac_Id.Equals(tag_adv.Mac_Id))
                            {
                                Debug.WriteLine("End - Start 중복 중복됨");
                                check_connect_exist = true;
                            }
                        }
                    }
                }

                Debug.WriteLine("Connect 갯수 :" + Connectors.Count);
                if (check_connect_exist == false)
                {
                    var connector = new Connector();
                    connector.Start = locator;
                    connector.End = tag_adv;
                    Connectors.Add(connector);
                }

            }));
        }


        private bool _showNames;
        public bool ShowNames
        {
            get { return _showNames; }
            set
            {
                _showNames = value;
                OnPropertyChanged("ShowNames");
            }
        }

        private bool _showCurrentCoordinates;
        public bool ShowCurrentCoordinates
        {
            get { return _showCurrentCoordinates; }
            set
            {
                _showCurrentCoordinates = value;
                OnPropertyChanged("ShowCurrentCoordinates");
            }
        }

        private bool _showAllCoordinates;
        public bool ShowAllCoordinates
        {
            get { return _showAllCoordinates; }
            set
            {
                _showAllCoordinates = value;
                OnPropertyChanged("ShowAllCoordinates");
            }
        }
        #region Creating New Objects

        private bool _creatingNewTag;
        public bool CreatingNewTag
        {
            get { return _creatingNewTag; }
            set
            {
                _creatingNewTag = value;
                OnPropertyChanged("CreatingNewTag");

                if (value)
                    CreateNewTag();
                else
                    RemoveNewObjects();
            }
        }

        public void CreateNewTag()
        {
            var newTag = new Tag()
            {
                Name = "Tag" + (Tags.Count + 1),
                IsNew = true
            };

            Tags.Add(newTag);
            SelectedObject = newTag;
        }

        public void RemoveNewObjects()
        {
            Tags.Where(x => x.IsNew).ToList().ForEach(x => Tags.Remove(x));
            Connectors.Where(x => x.IsNew).ToList().ForEach(x => Connectors.Remove(x));
        }

        private bool _creatingNewConnector;
        public bool CreatingNewConnector
        {
            get { return _creatingNewConnector; }
            set
            {
                _creatingNewConnector = value;
                OnPropertyChanged("CreatingNewConnector");

                if (value)
                    CreateNewConnector();
                else
                    RemoveNewObjects();
            }
        }

        public void CreateNewConnector()
        {
            var connector = new Connector()
            {
                Name = "Connector" + (Connectors.Count + 1),
                IsNew = true,
            };

            Connectors.Add(connector);
            SelectedObject = connector;
        }

        #endregion

        #region Delete Command

        private Command _deleteCommand;
        public Command DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new Command(Delete)); }
        }

        private void Delete()
        {
            if (SelectedObject is Connector)
                Connectors.Remove(SelectedObject as Connector);

            if (SelectedObject is Tag)
            {
                var tag = SelectedObject as Tag;
                Connectors.Where(x => x.Start == tag || x.End == tag).ToList().ForEach(x => Connectors.Remove(x));
                Tags.Remove(tag);
            }
        }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string _label_Group_Name = "0";
        public string Label_Group_Name
        {
            get { return _label_Group_Name; }
            set
            {
                _label_Group_Name = "GROUP : " + value;

                OnPropertyChanged("AreaWidth");
            }
        }


        #region Scrolling support

        private double _areaHeight = 2000;
        public double AreaHeight
        {
            get { return _areaHeight; }
            set
            {
                _areaHeight = value;
                OnPropertyChanged("AreaHeight");
            }
        }

        private double _areaWidth = 2000;


        public double AreaWidth
        {
            get { return _areaWidth; }
            set
            {
                _areaWidth = value;
                OnPropertyChanged("AreaWidth");
            }
        }

        public object ResourceHelper { get; }

        #endregion
    }
}
