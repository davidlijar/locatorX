using System.ComponentModel;
using System.Windows.Media;

namespace Controller_Tester.Standard_Simulation
{
    public abstract class DiagramObject : INotifyPropertyChanged
    {


        #region 공통
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private ImageSource _TagReceiveState;
        public ImageSource TagReceiveState
        {
            get { return _TagReceiveState; }
            set
            {
                _TagReceiveState = value;
                OnPropertyChanged("TagReceiveState");
            }
        }
        public int TagType { get; set; }

        public int TagGroup { get; set; }

        private string _mac_id;
        public string Mac_Id
        {
            get { return _mac_id; }
            set
            {
                _mac_id = value;
                OnPropertyChanged("Mac_Id");
            }
        }
        private string _ACK_Express;
        public string ACK_Express
        {
            get { return _ACK_Express; }
            set
            {
                _ACK_Express = value;
                OnPropertyChanged("ACK_Express");
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                OnPropertyChanged("IsNew");
            }
        }

        public abstract double X { get; set; }

        public abstract double Y { get; set; }
        #endregion





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}