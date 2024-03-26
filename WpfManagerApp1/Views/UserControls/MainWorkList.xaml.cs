using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfManagerApp1.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MainWorkList.xaml
    /// </summary>
    public partial class MainWorkList : UserControl
    {
        #region DependencyProperies

        public static readonly DependencyProperty WorkMouseDoubleClickCommandProperty =
            DependencyProperty.Register("WorkMouseDoubleClickCommand", typeof(ICommand), typeof(MainWorkList), new PropertyMetadata(null));

        public ICommand WorkMouseDoubleClickCommand
        {
            get { return (ICommand)GetValue(WorkMouseDoubleClickCommandProperty); }
            set { SetValue(WorkMouseDoubleClickCommandProperty, value); }
        }
        #endregion

        public MainWorkList()
        {
            InitializeComponent();
        }

        private void Work_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, frameworkElement.DataContext), DragDropEffects.Copy);
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WorkMouseDoubleClickCommand?.CanExecute(null) ?? false)
            {
                WorkMouseDoubleClickCommand?.Execute(null);
            }
        }
    }
}
