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


namespace Controller_Tester.Standard_Simulation
{



    public class TagListWindow_ViewModel : FrameworkElement, INotifyPropertyChanged
    {

        TagListWindow_Model TagListWindow_Model;


        public TagListWindow_ViewModel()
        {
            TagListWindow_Model = new TagListWindow_Model();

            TagListWindow_Model.tagList = new ObservableCollection<string>();

            //간격 리스트 추가
            //string No_Detect = "No Detect";
            //Add_TagList(No_Detect);

            //Select_TagList = No_Detect;
        }




        public void Add_TagList(string tag_id)
        {
            TagListWindow_Model.tagList.Add(tag_id);
        }

        public bool Check_TagID_inTagList(string tag_id)
        {
            bool ret_val = false;
            var Tag_Item = TagListWindow_Model.tagList.Where(x => x == tag_id).FirstOrDefault();
            if (Tag_Item != null)
            {
                //item이 있을 경우
                ret_val = true;
            }
            else
            {
                //item이 존재하지 않을 경우
                ret_val = false;
            }

            return ret_val;

        }

        void Clear_TagList()
        {
            if(TagListWindow_Model.tagList.Count != 0)
            {
                TagListWindow_Model.tagList.Clear();
            }
            
        }


        #region 바인딩 연결

        /// <summary>
        /// 전송 간격
        /// </summary>
        public ObservableCollection<string> TagList
        {
            get { return TagListWindow_Model.tagList; }
            set
            {
                TagListWindow_Model.tagList = value;
                NotifyPropertyChanged();
            }
        }
        public string Select_TagList
        {
            get { return TagListWindow_Model.select_TagList; }
            set
            {
                TagListWindow_Model.select_TagList = value;
                NotifyPropertyChanged();
            }
        }
        public int Select_TransIntervalList_Index
        {
            get { return TagList.IndexOf(TagListWindow_Model.select_TagList); }

            set
            {
                if (TagListWindow_Model.select_TagList_index != value)
                {
                    TagListWindow_Model.select_TagList_index = value;
                    TagListWindow_Model.select_TagList = TagList[value];
                    NotifyPropertyChanged();
                }

            }
        }


        #endregion


        #region  Button Command
        public ICommand TagListReset_Button => new RelayCommand<object>(TagListReset_Button_Run, null);
        private void TagListReset_Button_Run(object x)
        {

            Clear_TagList();

        }

        #endregion



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
