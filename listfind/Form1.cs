using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace listfind
{
    public partial class Form1 : Form
    {

        List<string> words = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files|*.txt|Comma-seperated values|*.csv|All files|*.*";

            ofd.ShowDialog();

            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                words = import(File.ReadAllLines(ofd.FileName));
            }
        }

        List<string> import(string[] lines)
        {
            List<string> wordlist = new List<string>();

            foreach (string line in lines)
            {
                wordlist.Add(line);
            }

            return wordlist;
        }

        Random rand = new Random();
        Timer T;
        int round_points = 0;
        int total_points;

        int StartRound()
        {
            const int max = 15;

            listBox1.Items.Clear();
            listBox1.ClearSelected();
            listBox1.Items.AddRange(GetWords(max));

            int rwi = rand.Next(max);
            string realword = words[rand.Next(words.Count)];

            label3.Text = realword;
            listBox1.Items.Insert(rwi, realword);

            return rwi;
        }

        string[] GetWords(int n)
        {
            List<string> rw = new List<string>();

            for (int i = 0; i < n; i++)
            {
                rw.Add(words[rand.Next(words.Count)]);
            }

            return rw.ToArray();
        }

        int answer;
        long round = 0;
        private void Button1_Click(object sender, EventArgs e)
        {
            label4.Text = "Round " + round.ToString();
            round++;
            answer = StartRound();
            round_points = 40;
            T.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (round_points > 0)
            {
                round_points--;
            }

            label1.Text = round_points.ToString();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == answer)
            {
                total_points += round_points;
                MessageBox.Show("This is correct!", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                T.Stop();
            }
            else
            {
                MessageBox.Show("This is incorrect!", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                round_points = 0;
                T.Stop();
            }


            label2.Text = total_points.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            T = new Timer();
            T.Tick += T_Tick;
            T.Interval = 100;
        }
    }
}
