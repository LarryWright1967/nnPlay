using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    public class Layer
    {
        //public Activations
        //Activations act = new Activations();
        Func<double, double> activate = (double x) => { return Activations.Sigmoid(x); }; 
        Func<double, double> activationDir = (double x) => { return Activations.ReverseSigmoid(x); };
        public List<Neuron> Neurons = new List<Neuron>();
        public Layer(/*int NeuronCt*/)
        {
            //for(int i = 0; i < NeuronCt; i++)
            //{

            //}
        }

        public void AddNeuron(List<double> weight)
        {
            Neurons.Add(new Neuron(weight));
        }

    }
}
