using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Controller_Tester.Standard_Simulation
{
    public class _3DViewerWindow_Model
    {
        public AxisAngleRotation3D rotationX { get; set; }
        public AxisAngleRotation3D rotationY { get; set; }
        public AxisAngleRotation3D rotationZ { get; set; }

        public string tagID { get; set; }
        public string azimuth { get; set; }
        public string eleveaiton { get; set; }

    }
}
