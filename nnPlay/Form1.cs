using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nnPlay
{
    public partial class Form1 : Form
    {
        Network network = new Network();
        public Form1()
        {
            InitializeComponent();
            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            int layerCount = 3;
            for(int i = 0; i < layerCount; i++)
            {
                
            }

            //network.AddLayer();
            //foreach (Layer l in network.getLayers())
            //{
            //    foreach (Neuron n in l.Neurons)
            //    {
            //        n.AddInput();
            //    }
            //}
        }
    }
}
