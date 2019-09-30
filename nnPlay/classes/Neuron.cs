using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    public class Neuron
    {
        private List<double> inputs = new List<double>();
        public double GetInputValue(int index) { return inputs[index]; }
        public void SetInputValue(int index, double value) { inputs[index] = value; }


        private List<double> weights = new List<double>();
        public double GetWeight(int index) { return weights[index]; }
        public void SetWeight(int index, double value) { weights[index] = value; }


        public double OutputValue { get; set; }

        public Neuron()
        {

        }

        public void AddInput() // previous layer outputs
        {
            inputs.Add(new double());
            weights.Add(new double());
        }

        /// <summary>
        /// forward pass
        /// </summary>
        /// <returns></returns>
        public void ForwardOutput()
        {
            // check if program has an impossible error
            if (inputs.Count != weights.Count) throw new Exception("input and weight counts don't match");

            double Temp = 0.0;
            for (int i = 0; i< inputs.Count; i++) { Temp += inputs[i] * weights[i]; }

            OutputValue = Activations.Sigmoid(Temp);
        }

        /// <summary>
        /// reverse pass
        /// </summary>
        /// <returns></returns>
        public void ReverseWeight(double ErrDev)
        {
            // check if program has an impossible error
            if (inputs.Count != weights.Count) throw new Exception("input and weight counts don't match");

            // calculate reverse function 
            // weighted error value from the output
            // applied to the input weights which are adjusted by
            // the input value and the previous weight

            for (int index = 0; index < inputs.Count; index++)
            {
                weights[index] = inputs[index] * weights[index] * ErrDev;
            }

            // how do we get the error value to use in this function?
        }
    }
}
