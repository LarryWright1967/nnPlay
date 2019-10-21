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
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Set(this, () =>
            {
                label2.Text = bools.Count().ToString();
                label3.Text = dubs.Count().ToString();
                label4.Text = (dubs.ReturnOneValue() * 255.0).ToString();
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
                int xSz = 255;
                int ySz = 255;
                Bitmap bm = new Bitmap(xSz, ySz, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                for (int x = 0; x < xSz; x++)
                {
                    for (int y = 0; y < ySz; y++)
                    {
                        double d = dubs.ReturnOneValue() * 255.0;
                        int I = (int)(Math.Floor(d));
                        Set(this, () => {label1.Text = I.ToString(); }); 
                        Color Col = Color.FromArgb(I, I, I);
                        bm.SetPixel(x, y, Col);
                    }
                }
                Set(this, () => { pictureBox1.BackgroundImage = bm; });
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
