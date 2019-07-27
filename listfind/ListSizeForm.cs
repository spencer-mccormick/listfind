using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace listfind
{
    public partial class ListSizeForm : Form
    {

        public int ListSize { get; set; }
        public ListSizeForm()
        {
            InitializeComponent();
        }

        private void ListSizeForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = ListSize;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ListSize = Convert.ToInt32(numericUpDown1.Value);
            Close();
        }
    }
}
