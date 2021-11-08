using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace endusukmaliyet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Point loc = this.panel1.Location;

            int X_coordinate = loc.X;
            int Y_coordinate = loc.Y;

            int satir = int.Parse(textBox1.Text);
            int sutun = int.Parse(textBox2.Text);

            panel1.Controls.Clear();

            for (int i = 0; i < satir + 1; i++)
            {
                X_coordinate = loc.X + 30;
                Y_coordinate = Y_coordinate + 30;

                Label l = new Label();
                Label ll = new Label();
                TextBox[] textBoxes = new TextBox[satir * sutun];

                for (int j = 0; j < sutun + 1; j++)
                {

                    textBoxes[i] = new TextBox();
                    textBoxes[i].Width = 30;
                    textBoxes[i].Height = 30;

                    X_coordinate += 35;

                    textBoxes[i].Text = (i + 1).ToString();
                    panel1.Controls.Add(textBoxes[i]);
                    panel1.Show();

                    if (j == sutun)
                    {
                        l.Text = "ARZ";
                        panel1.Controls.Add(l);
                        panel1.Show();
                        l.Location = new Point(X_coordinate, loc.Y + 30);
                    }
                    else if (i == satir)
                    {
                        ll.Text = "TALEP";
                        panel1.Controls.Add(ll);
                        panel1.Show();
                        ll.Location = new Point((loc.X), Y_coordinate + 35);
                    }

                    textBoxes[i].Location = new Point(X_coordinate, Y_coordinate + 30);

                }
            }
        }
        private void GetTextBoxStrings()
        {
            int satir = int.Parse(textBox1.Text);
            int sutun = int.Parse(textBox2.Text);

            //int[,] matris = new int[satir, sutun];
            //int[] arz = new int[satir * sutun];
            //int[] talep = new int[satir * sutun];
            //int[] satirx = new int[satir];
            //int[] sutunx = new int[sutun];

            int i = 1;
            int j = 1;
            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = c as TextBox;
                    MessageBox.Show("i: " + (i ).ToString() + "j: " + (j).ToString());

                    if (i % (sutun+1) == 0)
                    {
                        j++;
                    }

                    i++;
                }

            }
        }
        private void btnHesapla_Click(object sender, EventArgs e)
        {
            GetTextBoxStrings();
        }
    }
}
