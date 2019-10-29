using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        private Bitmap inputImage; // x, y; 20, 20 (1600B)
        private BitmapData bmpData; // x, y; 20, 20 (1200B)
        private byte[][][] layer1Filters; // x, y, filters; 3, 3, 100 (900B)(27 Inputs)(18 x 18 output)
        private byte[][][] layer2Filters; // x, y, filters; 3, 3, 100 (900B)(27 Inputs)(16 x 16 output)
        private byte[][][] layer3Filters; // x, y, filters; 3, 3, 100 (900B)(27 Inputs)(14 x 14 output)
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
                //label4.Text = (dubs.ReturnOneValue() * 255.0).ToString();
                //if (bools.Count() < 100000 || dubs.Count() < 100000)
                //{
                //    Set(this, () =>
                //    {
                //        ssl1.Text = "Waiting on random doubles.";
                //    });
                //}
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
            Task.Run(() =>
            {
                int xSz = 9;
                int ySz = 9;
                int r;
                int g;
                int b;

                //Set(this, () =>
                //{
                //    pictureBox1.Size = new Size(xSz * 8, ySz * 8);
                //});

                Bitmap bm = new Bitmap(xSz, ySz, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // grab random values
                int need = xSz * ySz /** 3*/;

                double[] d = GetDoubles(need);

                // set bit map values
                for (int y = 0; y < ySz; y++)
                {
                    for (int x = 0; x < xSz; x++)
                    {
                        r = (int)(Math.Floor(d[(y * (xSz/* * 3*/)) + (x/* * 3*/) + 0] * 255));
                        //g = (int)(Math.Floor(d[(y * (xSz * 3)) + (x * 3) + 1] * 255));
                        //b = (int)(Math.Floor(d[(y * (xSz * 3)) + (x * 3) + 2] * 255));
                        Color Col = Color.FromArgb(r, r, r/* g, b*/);
                        bm.SetPixel(x, y, Col);
                    }
                }

                // display bit map
                Set(this, () =>
                {
                    pictureBox2.Image = bm;
                    ssl1.Text = "Generating Data.";
                });
                //pictureBox2.BackgroundImage = bm;
                //pictureBox3.BackgroundImage = bm; 
            });
        }

        private unsafe void GenImgBut_Click(object sender, EventArgs e)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap.lockbits?view=netframework-4.8
            // int *ptr = & x;.  
            inputImage = new Bitmap(this.BackgroundImage, new Size(50, 40));
            bmpData = inputImage.LockBits(new Rectangle(new Point(0, 0), new Size(inputImage.Width, inputImage.Height)), System.Drawing.Imaging.ImageLockMode.ReadWrite, inputImage.PixelFormat);
            IntPtr bmpPnt = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * inputImage.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(bmpPnt, rgbValues, 0, bytes);
            double[] d = GetDoubles(inputImage.Width * inputImage.Height);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    int intoffset = (y * inputImage.Width) + x;
                    int byteoffset = (y * (inputImage.Width * 4)) + (x * 4);
                    int value = rgbValues[byteoffset] + ConvertRandToIntRange(d[intoffset], -16, 16);
                    if (value > 255) { value -= 16; } else if (value < 0) { value += 16; }
                    rgbValues[byteoffset] = (byte)value;
                    rgbValues[byteoffset + 1] = (byte)value;
                    rgbValues[byteoffset + 2] = (byte)value;
                }
            }
            //for (int counter = 2; counter < rgbValues.Length; counter += 4) { rgbValues[counter] = 255; }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, bmpPnt, bytes);
            inputImage.UnlockBits(bmpData);
            pictureBox1.Image = inputImage;
        }

        private int ConvertRandToIntRange(double d, int low, int high)
        {
            int range = high - low;
            int offset = low;
            int i = (int)(Math.Floor(d * range + offset));
            return i;
        }

        private double[] GetDoubles(int count)
        {
            bool enough = false;

            // ensure that enough values are available
            while (!enough)
            {
                if (dubs.Count() < count)
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
                        //ssl1.Text = "Processing...";
                    });
                    enough = true;
                }
            }

            // get values;
            double[] d = dubs.ReturnRangeOfValues(count).ToArray();

            return d;
            //return new double[] { 3.5, 4.5 };
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
