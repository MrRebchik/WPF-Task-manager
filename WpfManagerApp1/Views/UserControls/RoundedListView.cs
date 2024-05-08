using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfManagerApp1.Views.UserControls
{
    public class RoundedListView : ViewBase
    {
        private DataTemplate itemTemplate;
        public DataTemplate ItemTemplate
        {
            get { return itemTemplate; }
            set { itemTemplate = value; }
        }
        //protected override object DefaultStyleKey
        //{
        //    get { return new ComponentResourceKey(GetType(), "RoundedListView"); }
        //}
        //protected override object ItemContainerDefaultStyleKey
        //{
        //    get { return new ComponentResourceKey(GetType(), "RoundedListViewItem"); }
        //}
    }
}
