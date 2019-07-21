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
    public partial class StatsForm : Form
    {

        public List<Set> sets { get; set; }

        public StatsForm()
        {
            InitializeComponent();
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {
            StringBuilder S = new StringBuilder();
            foreach (Set set in sets)
            {
                S.AppendLine("Set (Total Score: " + set.set_points + ")");

                for (int i = 0; i < set.round_scores.Count; i++)
                {
                    S.AppendLine("    Round [" + i.ToString() + "]: " + set.round_scores[i].ToString());
                }
            }

            richTextBox1.Text = S.ToString();
        }
    }
}
