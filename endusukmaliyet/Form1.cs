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

            for (int i = 0; i < satir+1; i++)
            {
                X_coordinate = loc.X;
                Y_coordinate = Y_coordinate + 30;

                for (int j = 0; j < sutun+1; j++)
                {
                    TextBox a = new TextBox();
                    a.Width = 30;
                    a.Height = 30;

                    X_coordinate += 35;

                    a.Text = (i + 1).ToString();
                    panel1.Controls.Add(a);
                    panel1.Show();

                    a.Location = new Point(X_coordinate, Y_coordinate + 30);

                }
            }
        }
    }
}
