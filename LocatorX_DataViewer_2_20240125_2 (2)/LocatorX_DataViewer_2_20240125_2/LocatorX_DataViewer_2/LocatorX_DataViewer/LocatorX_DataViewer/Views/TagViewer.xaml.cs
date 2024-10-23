using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// tag_viewer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TagViewer : UserControl
    {
        public TagViewer()
        {
            InitializeComponent();

            /*NodeViewer 초기화*/
            ShareDataForClass.tagViewer_ViewModels = new ObservableCollection<TagViewer_ViewModel>();

            TagViewer_ViewModel nodeViewer_ViewModel = new TagViewer_ViewModel();
            string GroupName= "GROUP_1";
            nodeViewer_ViewModel.Set_pair_group_name(GroupName);
            nodeViewer_ViewModel.Label_Group_Name = GroupName;
            ShareDataForClass.tagViewer_ViewModels.Add(nodeViewer_ViewModel);

            //그룹 0를 인덱스로 한다.
            DataContext = ShareDataForClass.tagViewer_ViewModels[0];
            ShareDataForClass.tagViewer_ViewModels[0].Label_Group_Name = "0";
        }


        private void Thumb_Drag(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            if (thumb == null)
                return;

            var tag = thumb.DataContext as Tag;
            if (tag == null)
                return;

            tag.X += e.HorizontalChange;
            tag.Y += e.VerticalChange;
            System.Diagnostics.Debug.WriteLine("tag.X" + tag.X + "HorizontalChange" + e.HorizontalChange);
            System.Diagnostics.Debug.WriteLine("tag.Y" + tag.Y + "VerticalChange" + e.VerticalChange);
        }

        private void ListBox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var listbox = sender as ListBox;

            if (listbox == null)
                return;

            var vm = listbox.DataContext as TagViewer_ViewModel;

            if (vm == null)
                return;

            if (vm.SelectedObject != null && vm.SelectedObject is Tag && vm.SelectedObject.IsNew)
            {
                vm.SelectedObject.X = e.GetPosition(listbox).X;
                vm.SelectedObject.Y = e.GetPosition(listbox).Y;
            }
            else if (vm.SelectedObject != null && vm.SelectedObject is Connector && vm.SelectedObject.IsNew)
            {
                var tag = GetTagUnderMouse();
                if (tag == null)
                    return;

                var connector = vm.SelectedObject as Connector;

                if (connector.Start != null && tag != connector.Start)
                    connector.End = tag;
            }
        }
        private void ListBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var vm = DataContext as TagViewer_ViewModel;


            if (vm != null)
            {
                if (vm.CreatingNewConnector)
                {
                    var tag = GetTagUnderMouse();
                    var connector = vm.SelectedObject as Connector;
                    if (tag != null && connector != null && connector.Start == null)
                    {
                        connector.Start = tag;
                        tag.IsHighlighted = true;
                        e.Handled = true;
                        return;
                    }
                }
                //기존 선택된것 해제
                if (vm.SelectedObject != null)
                    vm.SelectedObject.IsNew = false;

                vm.CreatingNewTag = false;
                vm.CreatingNewConnector = false;
            }
        }

        private Tag GetTagUnderMouse()
        {
            var item = Mouse.DirectlyOver as ContentPresenter;
            if (item == null)
                return null;

            return item.DataContext as Tag;
        }



    }
}
