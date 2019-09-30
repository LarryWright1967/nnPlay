using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * error is the difference between the expected output and the actual output
 * https://youtu.be/kft1AJ9WVDk?t=637
 * ?? how to use the trained network in production?
 * 
 */

namespace nnPlay.classes
{
    class Network
    {
        List<Layer> layers = new List<Layer>();
        public Network()
        {

        }

        public void AddLayer(int NeuronCount)
        {
            int index = layers.Count;
            layers.Add(new Layer());
            for(int c = 0; c < NeuronCount; c++)
            {
                layers[index].AddNeuron();
            }
        }

        public void Forward()
        {
            foreach(Layer l in layers)
            {
                foreach(Neuron n in l.Neurons)
                {
                    n.ForwardOutput();
                }
            }
        }

        public void Reverse()
        {
            foreach (Layer l in layers)
            {
                foreach (Neuron n in l.Neurons)
                {
                    n.ReverseWeight(6);
                }
            }
        }

    }
}
