using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Controller_Tester.Standard_Simulation
{



    public class _3DViewerWindow_ViewModel : FrameworkElement, INotifyPropertyChanged
    {
        _3DViewerWindow_Model _3DViewerWindow_Model;


        private Point lastMousePosition;
        private bool isMousePressed = false;
        public RotateTransform3D RotateTransform { get; set; } = new RotateTransform3D();
        public Transform3DGroup TransformGroup { get; set; } = new Transform3DGroup();

        private AxisAngleRotation3D axisRotation = new AxisAngleRotation3D();
        int Timer_Tick_cntX = 180;
        int Timer_Tick_cntY = 0;

        public _3DViewerWindow_ViewModel()
        {
            _3DViewerWindow_Model = new _3DViewerWindow_Model();

            Initialize3DObjectRotation();

#if false
            // 타이머 생성
            DispatcherTimer timer = new DispatcherTimer();

            // 타이머 간격을 1초로 설정
            timer.Interval = TimeSpan.FromSeconds(1);

            // 타이머 이벤트 핸들러 연결
            timer.Tick += Timer_Tick;

            // 타이머 시작
            timer.Start();
#endif

            _3DViewerWindow_Model.rotationX = new AxisAngleRotation3D();
            _3DViewerWindow_Model.rotationY = new AxisAngleRotation3D();
            _3DViewerWindow_Model.rotationZ = new AxisAngleRotation3D();

        Rotate3DObject(0, 0);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 여기에 1초마다 실행될 로직을 구현합니다.
            // 예: UI 업데이트, 변수 체크, 로그 기록 등
            Timer_Tick_cntX+=10;
            Timer_Tick_cntY += 10;
            if (Timer_Tick_cntX > 180)
            {
                Timer_Tick_cntX = -180;
            }
            if (Timer_Tick_cntY > 90)
            {
                Timer_Tick_cntY = -90;
            }

            Rotate3DObject(Timer_Tick_cntX, 0);

            Azimuth = Timer_Tick_cntX.ToString();
            Elevation = Timer_Tick_cntY.ToString();
        }

        private void Initialize3DObjectRotation()
        {
            //var axisRotation = new AxisAngleRotation3D(new Vector3D(1, 1, 0), 0);
            axisRotation.Axis = new Vector3D(1, 1, 0);
            RotateTransform.Rotation = axisRotation;
            TransformGroup.Children.Add(RotateTransform);
        }

#if false
        public void OnMouseDown(Point position)
        {
            lastMousePosition = position;
            isMousePressed = true;
        }

        public void OnMouseUp()
        {
            isMousePressed = false;
        }

        public void OnMouseMove(Point position)
        {
            if (isMousePressed)
            {
                double deltaX = position.X - lastMousePosition.X;
                double deltaY = position.Y - lastMousePosition.Y;
                Rotate3DObject(deltaX, deltaY);
                lastMousePosition = position;
            }
        }
        private void Rotate3DObject(double deltaX, double deltaY)
        {
            var axisRotation = RotateTransform.Rotation as AxisAngleRotation3D;
            axisRotation.Angle += deltaX;
            var rotationY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), deltaY);
            TransformGroup.Children.Add(new RotateTransform3D(rotationY));
        }

#endif



        public AxisAngleRotation3D RotationX
        {
            get => _3DViewerWindow_Model.rotationX;
            set
            {
                _3DViewerWindow_Model.rotationX = value;
                NotifyPropertyChanged();
            }
        }


        public AxisAngleRotation3D RotationY
        {
            get => _3DViewerWindow_Model.rotationY;
            set
            {
                _3DViewerWindow_Model.rotationY = value;
                NotifyPropertyChanged();
            }
        }


        public AxisAngleRotation3D RotationZ
        {
            get => _3DViewerWindow_Model.rotationZ;
            set
            {
                _3DViewerWindow_Model.rotationZ = value;
                NotifyPropertyChanged();
            }
        }

        public string TagID
        {
            get { return _3DViewerWindow_Model.tagID; }
            set
            {
                _3DViewerWindow_Model.tagID = value;
                NotifyPropertyChanged();
            }
        }
        public string Azimuth
        {
            get { return _3DViewerWindow_Model.azimuth; }
            set
            {
                _3DViewerWindow_Model.azimuth = value;
                NotifyPropertyChanged();
            }
        }

        public string Elevation
        {
            get { return _3DViewerWindow_Model.eleveaiton; }
            set
            {
                _3DViewerWindow_Model.eleveaiton = value;
                NotifyPropertyChanged();
            }
        }


        public double NormalizeAngle(double angle)
        {
            while (angle > 180) angle -= 360;
            while (angle < -180) angle += 360;
            return angle;
        }

        public void Rotate3DObject(double azimuth, double elevation)
        {
            double normalizedAzimuth = NormalizeAngle(azimuth);
            double normalizedElevation = NormalizeAngle(elevation);

 
            RotationZ.Angle = normalizedAzimuth;


            RotationX.Angle = normalizedElevation*(-1);
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
