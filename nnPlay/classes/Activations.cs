using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    // https://youtu.be/ILsA4nyG7I0
    // https://youtu.be/kft1AJ9WVDk

    //public static SIGMOID: ActivationFunction = {
    //output: x => 1 / (1 + Math.exp(-x)),
    //der: x => {
    //let output = Activations.SIGMOID.output(x);
    //return output * (1 - output);
    //}
    //};
    //σ(x)⋅(1−σ(x))
    // x*(1-x)?
    public static class Activations
    {
        public static double Sigmoid(double inputValue)
        {
            return 1 / (1 + Math.Exp(-inputValue));
        }
        public static double ReverseSigmoid(double output) // gradient
        {
            return output * (1.0 - output);
        }
        public static double Relu(double inputValue)
        {
            if (inputValue > 0) { return inputValue; } else { return 0; }
        }
        public static double ReverseReLu(double output) // gradient
        {
            if (output > 0) { return 1; } else { return 0; }
        }
    }
}
