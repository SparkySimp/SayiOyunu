using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SparkySimp.NTP34SayıOyunu
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        byte[] numbers = new byte[9];
        Random prng = new Random();
        private void MainForm_Load(object sender, EventArgs e)
        {
            var buttons = (from Control ctl in this.Controls where ctl is Button _ select ctl).ToArray();
            for (int i = 0; i < buttons.Count(); i++)
            {
                buttons[i].Text = "";
                buttons[i].Tag = (object)i;
                var sayi = (byte)prng.Next(1, 10);
                while(Array.IndexOf(numbers, sayi) != -1)
                    sayi = (byte)prng.Next(1, 10);

                numbers[i] = sayi;
            }

        }
        int currNum = 0, streak = 0;

        private void Form1_MouseHover(object sender, EventArgs e)
        {

        }

        int lastClickedButtonNum = 0;

        private void MainGameTimer_Tick(object _, EventArgs e)
        {
            label1.Text = (int.Parse(label1.Text) + 1).ToString();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
                foreach (var item in from Control ctl in this.Controls where ctl is Button _ select 0)
                {

                }
                
        }

        private void NumberButton_Click(object sender, EventArgs _)
        {
            int btnId = 0;
            var buttons = (from Control ctl in this.Controls where ctl is Button _ select ctl).ToArray();
            if (!tmGameCycleTimer.Enabled) tmGameCycleTimer.Start();
            if (((Control)sender).Text == String.Empty)
            {
                foreach (var num in numbers)
                {
                    buttons[btnId].Text = num.ToString();
                    btnId++;
                }
                return;
            }
            else
            {
                if (int.Parse(((Control)sender).Text) == lastClickedButtonNum + 1)
                {
                    ((Control)sender).BackColor = Color.LimeGreen;
                    lastClickedButtonNum++;
                }
                else
                {
                    ((Control)sender).BackColor = Color.Red;
                    lastClickedButtonNum = 0;
                    tmGameCycleTimer.Stop();
                    Task.Delay(1000).ConfigureAwait(true).GetAwaiter().GetResult();
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].BackColor = SystemColors.Control;
                    }
                    tmGameCycleTimer.Start();
                }
            }

            if (lastClickedButtonNum == 9) tmGameCycleTimer.Stop();
        }
    }
}
