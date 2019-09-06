using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    class Neuron
    {
        double[] inputs;
        double[] weights;
        double returnVal;
        public double Output()
        {
            if (inputs.Length != weights.Length) return 0.0;
            returnVal = 0.0;
            for (int i = 0; i< inputs.Length; i++)
            {
                returnVal += inputs[i] * weights[i];
            }
            returnVal = Activations.Sigmoid(returnVal);
            return returnVal;
        }
    }
}
