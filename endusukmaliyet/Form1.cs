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

        private void button1_Click(object sender, EventArgs e) //oluştur butonu
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
                TextBox[] textBoxes = new TextBox[satir+1 * sutun+1];

                for (int j = 0; j < sutun + 1; j++)
                {

                    textBoxes[i] = new TextBox();
                    textBoxes[i].Width = 30;
                    textBoxes[i].Height = 30;

                    X_coordinate += 35;

                    textBoxes[i].Text = (i + 1).ToString();

                    panel1.Controls.Add(textBoxes[i]);
                    panel1.Show();

                    //Arz ve Talep sütununlarının kesiştiği noktadaki textBox disabled edildi:
                    if (i == satir && j==sutun)
                    {
                        textBoxes[i].Text = "";
                        textBoxes[i].Enabled = false;
                    }

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

            int[,] matris = new int[satir + 1, sutun + 1];
            int[] arz = new int[satir * sutun];
            int[] talep = new int[sutun * satir];
            //int[] satirx = new int[satir];
            //int[] sutunx = new int[sutun];

            int i = 1;
            int j = 1;
            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    int yeniSatir = j;
                    int yeniSutun = i % (sutun + 1) == 0 ? sutun + 1 : i % (sutun + 1);
                    TextBox tb = c as TextBox;

                    //matris'e textBox verilerini aktarma kısmı
                    if (yeniSatir != satir + 1)
                    {
                        if (yeniSutun != sutun + 1)
                        {
                            matris[yeniSatir, yeniSutun] = int.Parse(tb.Text);
                        }
                    }

                    //Arz dizisine textBox verilerini aktarma kısmı
                    if (yeniSutun == sutun + 1 &&  yeniSatir != satir+1)
                    {
                        arz[yeniSutun] = int.Parse(tb.Text);
                    }

                    //Talep dizisine textBox verilerini aktarma kısmı
                    if (yeniSatir == satir + 1 && yeniSutun != sutun + 1 )
                    {
                        talep[yeniSatir] = int.Parse(tb.Text);
                    }

                    if (i % (sutun + 1) == 0)
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
