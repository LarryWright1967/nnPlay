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

namespace nnPlay
{
    public class Network
    {
        private List<Layer> layers = new List<Layer>();
        public List<Layer> getLayers() { return new List<Layer>(layers); }
        public Network()
        {

        }

        public void AddLayer(List<Neuron> neurons)
        {
            //int index = layers.Count;
            //layers.Add(new Layer(6));
            //for(int c = 0; c < NeuronCount; c++)
            //{
            //    layers[index].AddNeuron();
            //}
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
