using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace GridView行选复制单元格
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> list = new ObservableCollection<Person>();
        public MainWindow()
        {
            InitializeComponent();
            for (var i = 0; i < 5; i++)
            {
                list.Add(new Person()
                {
                    别名 = $"这是一个别名{i}",
                    地址 = "哈尔滨",
                    备注 = "测试",
                    姓名 = $"张{i}",
                    年龄 = i,
                    性别 = "男",
                    身份证号码 = i.ToString()
                });
            }
            this.DataContext = list;
            //  grid.ItemsSource = list;
        }

        bool able = true;
        private void grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //grid.SelectAllCells(); 
            var cells = grid.SelectedCells;
            var index = list.IndexOf(((DataGridCellInfo)e.AddedCells[0]).Item as Person);
            //for (var i = 0; i < grid.Items.Count; i++)
            //{
            //    ((DataGridRow)this.grid.ItemContainerGenerator.ContainerFromIndex(i)).Background = new SolidColorBrush(Colors.White);
            //}
            //DataGridRow row1 = (DataGridRow)this.grid.ItemContainerGenerator.ContainerFromIndex(index);
            //if (row1 != null)
            //    row1.Background = new SolidColorBrush(Colors.LightSkyBlue);
            CopyCell(sender, null);
            grid.SelectedIndex = index;
            able = true;
        } 
        private void CopyCell(object sender, RoutedEventArgs e)
        {
            if (!able) return;
            if (grid.SelectedItem is Person)
            {
                var item = grid.SelectedItem;
            }
            var list = grid.SelectedCells;
            var cellinfo = list.LastOrDefault();
            if (cellinfo == null || cellinfo.Column == null) return;
            var text = ((TextBlock)cellinfo.Column.GetCellContent(cellinfo.Item)).Text;
            Clipboard.SetText(text);
            able = false;
        }

        private void SelectRow(object sender, RoutedEventArgs e)
        {
            //var index = list.IndexOf(((DataGridCellInfo)e.AddedCells[0]).Item as Person);
            //grid.SelectedIndex = index;
        }

        private void SelectTable(object sender, RoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var list = grid.SelectedCells;
            var cellinfo = list.LastOrDefault();
            if (cellinfo == null || cellinfo.Column == null)
            {
                e.Handled = true;
                return;
            }
            var text = ((TextBlock)cellinfo.Column.GetCellContent(cellinfo.Item)).Text;
            Clipboard.SetText(text);
            e.Handled = true;
        }
    }
}
