using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongNet
{
    public partial class TopScores : Form
    {
        public TopScores()
        {
            InitializeComponent();
            fillTable(scoreTable, getContent());
        }

        public void fillTable(TableLayoutPanel table, Label[,] content)
        {
            for(int i = 0; i < table.ColumnCount; i++)
            {
                for (int j = 0; j < table.RowCount; j++)
                {
                    table.Controls.Add(content[i, j], i, j);
                }
            }
        }

        public Label[,] getContent()
        {
            Label[,] content = new Label[scoreTable.ColumnCount, scoreTable.RowCount];
            for (int i = 0; i < scoreTable.ColumnCount; i++)
            {
                for (int j = 0; j < scoreTable.RowCount; j++)
                {
                    content[i, j] = new Label();
                    content[i, j].Text = "asd";
                }
            }
            return content;
        }
    }
}
