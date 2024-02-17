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
    /// Логика взаимодействия для EisenhowerMatrixCell.xaml
    /// </summary>
    public partial class EisenhowerMatrixCell : UserControl
    {
        public static readonly DependencyProperty WorkDropCommandProperty =
            DependencyProperty.Register("WorkDropCommand", typeof(ICommand), typeof(EisenhowerMatrixCell), new PropertyMetadata(null));

        public ICommand WorkDropCommand
        {
            get { return (ICommand)GetValue(WorkDropCommandProperty); }
            set { SetValue(WorkDropCommandProperty, value); }
        }
        public EisenhowerMatrixCell()
        {
            InitializeComponent();
        }

        private void EisenhowerMatrixCell_Drop(object sender, DragEventArgs e)
        {
            if (WorkDropCommand?.CanExecute(null) ?? false)
            {
                WorkDropCommand?.Execute(null);
            }
        }

        private void WorkItem_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, 
                    new DataObject(DataFormats.Serializable, 
                    frameworkElement.DataContext),
                    DragDropEffects.Move);
            }
        }
    }
}
