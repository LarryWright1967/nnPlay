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
    public static class Activations
    {
        public static double Sigmoid(double inputValue)
        {
            return 1 / (1 + Math.Exp(-inputValue));
        }
        public static double ReverseSigmoid1(double error)
        {
            return double.NaN;
        }
        public static double ReverseSigmoid2(double error)
        {
            return double.NaN;
        }
    }
}
