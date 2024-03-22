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


        #region Dependencies

        #region IncomingWorkItem
        public static readonly DependencyProperty IncomingWorkItemProperty =
            DependencyProperty.Register("IncomingWorkItem", typeof(object), typeof(EisenhowerMatrixCell),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object IncomingWorkItem
        {
            get { return (object)GetValue(IncomingWorkItemProperty); }
            set { SetValue(IncomingWorkItemProperty, value); }
        }
        #endregion

        #region WorkDropCommand
        public static readonly DependencyProperty WorkDropCommandProperty =
           DependencyProperty.Register("WorkDropCommand", typeof(ICommand), typeof(EisenhowerMatrixCell), new PropertyMetadata(null));

        public ICommand WorkDropCommand
        {
            get { return (ICommand)GetValue(WorkDropCommandProperty); }
            set { SetValue(WorkDropCommandProperty, value); }
        }
        #endregion

        #region RemovedWorkItem
        public static readonly DependencyProperty RemovedWorkItemProperty =
            DependencyProperty.Register("RemovedWorkItem", typeof(object), typeof(EisenhowerMatrixCell),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object RemovedWorkItem
        {
            get { return (object)GetValue(RemovedWorkItemProperty); }
            set { SetValue(RemovedWorkItemProperty, value); }
        }
        #endregion

        #region WorkRemoveCommand
        public static readonly DependencyProperty WorkRemoveCommandProperty =
            DependencyProperty.Register("WorkRemoveCommand", typeof(ICommand), typeof(EisenhowerMatrixCell), new PropertyMetadata(null));

        public ICommand WorkRemoveCommand
        {
            get { return (ICommand)GetValue(WorkRemoveCommandProperty); }
            set { SetValue(WorkRemoveCommandProperty, value); }
        } 
        #endregion

        #endregion
        public EisenhowerMatrixCell()
        {
            InitializeComponent();
        }

        private void EisenhowerMatrixCell_Drop(object sender, DragEventArgs e)
        {
            if (WorkDropCommand?.CanExecute(null) ?? false)
            {
                IncomingWorkItem = e.Data.GetData(DataFormats.Serializable);
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

        private void ListView_DragLeave(object sender, DragEventArgs e)
        {
            if (WorkRemoveCommand?.CanExecute(null) ?? false)
            {
                RemovedWorkItem = e.Data.GetData(DataFormats.Serializable);
                WorkRemoveCommand?.Execute(null);
            }
        }
    }
}
