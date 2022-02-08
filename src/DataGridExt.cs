using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GridView行选复制单元格
{
    public class DataGridExt : DataGrid
    {
        private bool IsCopying = false;
        public string CellText = string.Empty;
        public DataGridExt()
        {
            this.SelectedCellsChanged += DataGridExt_SelectedCellsChanged;
            this.KeyUp += DataGridExt_KeyUp;
        }

        private void DataGridExt_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.C && !string.IsNullOrEmpty(CellText))
            {
                Clipboard.SetText(CellText);
                CellText = string.Empty;
                e.Handled = false;
            }
        }

        private void DataGridExt_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var cells = this.SelectedCells;
            if (cells == null || cells.Count == 0) return;
            var index = this.Items.IndexOf(cells.First().Item);
            CellText = GetCellText();
            this.SelectedIndex = index;
            IsCopying = false;
        }
        private string GetCellText()
        {
            if (IsCopying) return CellText;
            IsCopying = true;
            var list = this.SelectedCells;
            var cellinfo = list.LastOrDefault();
            if (cellinfo == null || cellinfo.Column == null) return CellText;
            if (cellinfo.Column.GetType() == typeof(DataGridTextColumn))
            {
                var text = ((TextBlock)cellinfo.Column.GetCellContent(cellinfo.Item)).Text;
                return text;
            }
            //如果需要处理其他类型的列，请在这里添加更多的if分支或者增加决策类

            return String.Empty;
        }
    }
}
