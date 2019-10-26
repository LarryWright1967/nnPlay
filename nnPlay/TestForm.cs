using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RandGen;

namespace nnPlay
{
    public partial class TestForm : Form
    {
        private readonly ListOfRandBools bools = new ListOfRandBools();
        private readonly ListOfRandDouble_ZeroToOne dubs = new ListOfRandDouble_ZeroToOne();
        Timer t;
        public TestForm()
        {
            this.Shown += TestForm_Shown;
            InitializeComponent();
        }

        private void TestForm_Shown(object sender, EventArgs e)
        {
            GenImgBut.Click += GenImgBut_Click;
            ReConvoBut.Click += ReConvoBut_Click;
            ForwardBut.Click += ForwardBut_Click;
            ReverseBut.Click += ReverseBut_Click;
            t = new Timer();
            t.Interval = 333;
            t.Tick += T_Tick;
            t.Start();
            Set(this, () =>
            {
                ssl1.Text = "Generating Data.";
            });
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Set(this, () =>
            {
                label2.Text = bools.Count().ToString();
                label3.Text = dubs.Count().ToString();
                label4.Text = (dubs.ReturnOneValue() * 255.0).ToString();
                if (bools.Count() < 100000 || dubs.Count() < 100000)
                {
                    //Set(this, () =>
                    //{
                    //    ssl1.Text = "Waiting on random doubles.";
                    //});
                }
            });
        }

        private void ReverseBut_Click(object sender, EventArgs e)
        {
        }

        private void ForwardBut_Click(object sender, EventArgs e)
        {
        }

        private void ReConvoBut_Click(object sender, EventArgs e)
        {
        }

        private void GenImgBut_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                int xSz = 20;
                int ySz = 20;
                int i = 0;
                int r;
                int g;
                int b;

                Set(this, () =>
                {
                    pictureBox1.Size = new Size(xSz * 8, ySz * 8);
                });

                Bitmap bm = new Bitmap(xSz, ySz, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // grab random values
                int need = xSz * ySz * 3;
                bool enough = false;

                // ensure that enough values are available
                while (!enough)
                {
                    if (dubs.Count() < need)
                    {
                        Set(this, () =>
                        {
                            ssl1.Text = "Waiting on random doubles.";
                        });
                        System.Threading.Thread.Sleep(250);
                    }
                    else
                    {
                        Set(this, () =>
                        {
                            ssl1.Text = "Processing...";
                        });
                        enough = true;
                    }
                }

                // get values;
                double[] d = dubs.ReturnRangeOfValues(need).ToArray();

                // set bit map values
                i = -1;
                for (int y = 0; y < ySz; y++)
                {
                    for (int x = 0; x < xSz; x++)
                    {
                        //int I = (int)(Math.Floor(d[(x * y) + x] * 255));
                        i++;
                        r = (int)(Math.Floor(d[i] * 255));
                        i++;
                        g = (int)(Math.Floor(d[i] * 255));
                        i++;
                        b = (int)(Math.Floor(d[i] * 255));
                        Color Col = Color.FromArgb(r, g, b);
                        bm.SetPixel(x, y, Col);
                    }
                }

                // display bit map
                Set(this, () =>
                {
                    //pictureBox1.BackgroundImage = bm;
                    pictureBox1.Image = bm;
                    ssl1.Text = "Generating Data.";
                });
                //pictureBox2.BackgroundImage = bm;
                //pictureBox3.BackgroundImage = bm; 
            });
        }

        #region set
        public void Set(Control c, Action a)
        {
            if (c != null && !c.IsDisposed && c.IsHandleCreated)
            {
                if (c.InvokeRequired)
                {
                    c.BeginInvoke(a);
                }
                else
                {
                    a();
                }
            }
        }
        #endregion
    }
}
