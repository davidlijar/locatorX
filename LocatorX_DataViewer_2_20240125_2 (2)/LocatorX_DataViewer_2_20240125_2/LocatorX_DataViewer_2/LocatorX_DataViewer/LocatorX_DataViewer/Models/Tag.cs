using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Controller_Tester.Standard_Simulation
{
    public class Tag : DiagramObject
    {
        public enum TagTypeEnum
        {
            INIT_NONE,
            LOCATROR,
            BLE_TAG
        }


        #region 노드 공통
        private double _x;
        public Tag()
        {

        }
        public override double X
        {
            get { return _x; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 50.
                //_x = (Math.Round(value / 50.0)) * 50;
                _x = (Math.Round(value / 10.0)) * 10;
                //_x = (Math.Round(value / 1.0)) * 1;
                OnPropertyChanged("X");
            }
        }

        private double _y;
        public override double Y
        {
            get { return _y; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 50.
                //_y = (Math.Round(value / 50.0)) * 50;
                _y = (Math.Round(value / 10.0)) * 10;
                //_y = (Math.Round(value / 1.0)) * 1;
                OnPropertyChanged("Y");
            }
        }
        double _rssi;
        public double Rssi
        {
            get
            {
                return _rssi;
            }
            set
            {
                _rssi = value;
                OnPropertyChanged("Rssi");
            }
        }
        private bool _isHighlighted { get; set; }
        public bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }
            set
            {
                _isHighlighted = value;
                OnPropertyChanged("IsHighlighted");
            }
        }

        public ImageSource TagState_Normal { get; set; }
        public ImageSource TagState_Select { get; set; }
        public ImageSource TagState_Move { get; set; }

        #endregion

    }
}