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
            ofd.Filter = "Text files|*.txt|All files|*.*";

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
        List<long> running_points = new List<long>();
        List<Set> game_sets = new List<Set>();

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
            running_points.Add(round_points);
            round_points = 40;
            T.Start();
            button1.Enabled = false;
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
            T.Stop();

            if (listBox1.SelectedIndex == answer)
            {
                total_points += round_points;
                MessageBox.Show("This is correct!", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("This is incorrect!", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                round_points = 0;
            }

            button1.Enabled = true;
            label1.Text = round_points.ToString();
            label2.Text = total_points.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            T.Stop();
            game_sets.Add(new Set(running_points, total_points, round));

            round = 0;
            round_points = 0;
            running_points = new List<long>();
            total_points = 0;
            label3.Text = "Reset";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            T = new Timer();
            T.Tick += T_Tick;
            T.Interval = 100;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            StatsForm sf = new StatsForm();
            sf.sets = game_sets;
            sf.ShowDialog();
        }
    }

    public class Set
    {
        public List<long> round_scores { get; set; }
        public long set_points { get; set; }
        public long set_rounds { get; set; }
        public Set(List<long> scores, long totalpoints, long rounds)
        {
            round_scores = scores;
            set_points = totalpoints;
            set_rounds = rounds;
        }
    }
}
