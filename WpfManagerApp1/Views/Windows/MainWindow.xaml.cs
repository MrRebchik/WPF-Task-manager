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

namespace WpfManagerApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty WorkDropCommandProperty =
            DependencyProperty.Register("WorkDropCommand", typeof(ICommand), typeof(MainWindow), new PropertyMetadata(null));

        public ICommand WorkDropCommand
        {
            get { return (ICommand)GetValue(WorkDropCommandProperty); }
            set { SetValue(WorkDropCommandProperty, value); }
        }

        //public static readonly DependencyProperty WorkMouseDoubleClickCommandProperty =
        //    DependencyProperty.Register("WorkDropCommand", typeof(ICommand), typeof(MainWindow), new PropertyMetadata(null));

        //public ICommand WorkMouseDoubleClickCommand
        //{
        //    get { return (ICommand)GetValue(WorkMouseDoubleClickCommandProperty); }
        //    set { SetValue(WorkMouseDoubleClickCommandProperty, value); }
        //}

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Work_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, frameworkElement.DataContext), DragDropEffects.Copy);
            }
        }

        private void WorkListView_Drop(object sender, DragEventArgs e)
        {
            if (WorkDropCommand?.CanExecute(null) ?? false)
            {
                WorkDropCommand?.Execute(null);
            }
        }

        //private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (WorkMouseDoubleClickCommand?.CanExecute(null) ?? false)
        //    {
        //        WorkMouseDoubleClickCommand?.Execute(null);
        //    }
        //}
    }
}
