using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    public class Neuron
    {
        private double[] inputValues;
        public double GetInputValue(int index) { return inputValues[index]; }
        public void SetInputValue(int index, double value) { inputValues[index] = value; }

        private double[] weights;
        public double GetWeight(int index) { return weights[index]; }
        public void SetWeight(int index, double value) { weights[index] = value; }

        public double OutputValue { get; set; }

        public Neuron()
        {

        }

        public Neuron(int[] inputs, int[] weights)
        {
            if (inputs.Length != weights.Length) throw new ArgumentException("input and weight counts don't match");
        }

        /// <summary>
        /// forward pass
        /// </summary>
        /// <returns></returns>
        public double AdjustOutput()
        {
            // check if program has an impossible error
            if (inputValues.Length != weights.Length) throw new Exception("input and weight counts don't match");

            OutputValue = 0.0;
            for (int i = 0; i< inputValues.Length; i++) { OutputValue += inputValues[i] * weights[i]; }

            OutputValue = Activations.Sigmoid(OutputValue);
            return OutputValue;
        }

        /// <summary>
        /// reverse pass
        /// </summary>
        /// <returns></returns>
        public double AdjustWeights()
        {
            // calculate reverse function 
            // weighted error value from the output
            // applied to the input weights which are adjusted by
            // the input value and the previous weight

            // how do we get the error value to use in this function?
            return double.NaN;
        }
    }
}
