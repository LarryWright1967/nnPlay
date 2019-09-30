using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    public class Layer
    {
        public List<Neuron> Neurons = new List<Neuron>();
        public Layer()
        {

        }

        public void AddNeuron()
        {
            Neurons.Add(new Neuron());
        }

    }
}
