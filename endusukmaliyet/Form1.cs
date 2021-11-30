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
                TextBox[] textBoxes = new TextBox[satir + 1 * sutun + 1];

                for (int j = 0; j < sutun + 1; j++)
                {

                    textBoxes[i] = new TextBox();
                    textBoxes[i].Width = 30;
                    textBoxes[i].Height = 30;  

                    X_coordinate += 35;

                    //textBoxes[i].Text = (i + 1).ToString();

                    panel1.Controls.Add(textBoxes[i]);
                    panel1.Show();

                    if(j==sutun || i==satir)
                    {
                        textBoxes[i].BackColor = Color.MistyRose;
                    }

                    //Arz ve Talep sütununlarının kesiştiği noktadaki textBox disabled edildi:
                    if (i == satir && j == sutun)
                    {
                        textBoxes[i].Text = "";
                        textBoxes[i].Enabled = false;
                        textBoxes[i].BackColor = panel1.BackColor;
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

            int[,] matris = new int[satir, sutun];
            int[] arz = new int[satir];
            int[] talep = new int[sutun];
            int[] rf = new int[satir];
            int[] cf = new int[sutun];
            int min, b, d, p = 0, q = 0, c1, c2, sum = 0;

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
                            matris[yeniSatir - 1, yeniSutun - 1] = int.Parse(tb.Text);

                        }
                    }

                    //Arz dizisine textBox verilerini aktarma kısmı       
                    if (yeniSutun == sutun + 1 && yeniSatir != satir + 1)
                    {
                        //dizi 0. indeksten başlar diye yeniSatir-1 yapıyorum:
                        arz[yeniSatir - 1] = int.Parse(tb.Text);

                    }

                    //Talep dizisine textBox verilerini aktarma kısmı
                    if (yeniSatir == satir + 1 && yeniSutun != sutun + 1)
                    {
                        //dizi 0. indeksten başlar diye yeniSutun-1 yapıyorum:
                        talep[yeniSutun - 1] = int.Parse(tb.Text);

                    }

                    if (i % (sutun + 1) == 0)
                    {
                        j++;
                    }

                    i++;

                }
            }
            ///En düşük maliyet hesaplanması algoritma kısmı
            for (i = 0; i < satir; i++)
            {
                rf[i] = 0;
            }
            for (i = 0; i < sutun; i++)
            {
                cf[i] = 0;
            }
            b = satir;
            d = sutun;


            while (b > 0 && d > 0)
            {
                min = 1000;
                for (i = 0; i < satir; i++)
                {
                    if (rf[i] != 1)
                    {
                        for (j = 0; j < sutun; j++)
                        {
                            if (cf[j] != 1)
                            {
                                if (min > matris[i, j])
                                {
                                    min = matris[i, j];
                                    p = i;
                                    q = j;

                                }
                            }
                        }
                    }
                }
                if (arz[p] < talep[q])
                {
                    c1 = arz[p];

                }
                else
                {
                    c1 = talep[q];

                }


                for (i = 0; i < satir; i++)
                {
                    if (rf[i] != 1)
                    {
                        for (j = 0; j < sutun; j++)
                        {
                            if (cf[j] != 1)

                            {
                                if (min == matris[i, j])
                                {
                                    if (arz[i] < talep[j])
                                    {
                                        c2 = arz[i];
                                    }
                                    else
                                    {
                                        c2 = talep[j];
                                    }
                                    if (c2 > c1)
                                    {
                                        c1 = c2;
                                        p = i;
                                        q = j;
                                    }
                                }
                            }
                        }
                    }
                }

                if (arz[p] < talep[q])
                {
                    sum += matris[p, q] * arz[p];
                    talep[q] -= arz[p];
                    rf[p] = 1;
                    b--;
                }
                else if (arz[p] > talep[q])
                {
                    sum = sum + matris[p, q] * talep[q];
                    arz[p] -= talep[q];
                    cf[q] = 1;
                    d--;
                }
                else if (arz[p] == talep[q])
                {
                    sum = sum + matris[p, q] * arz[p];
                    rf[p] = 1;
                    cf[q] = 1;
                    b--;
                    d--;
                }
            }

            lbl_sonuc.Text = sum.ToString();
            MessageBox.Show("Toplam maliyet: " + sum);
         
        }
        private void btnHesapla_Click(object sender, EventArgs e)
        {
            GetTextBoxStrings();
        }

       
    }
}
