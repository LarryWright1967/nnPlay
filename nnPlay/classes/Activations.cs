using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    https://youtu.be/ILsA4nyG7I0

    //public static SIGMOID: ActivationFunction = {
    //output: x => 1 / (1 + Math.exp(-x)),
    //der: x => {
    //let output = Activations.SIGMOID.output(x);
    //return output * (1 - output);
    //}
    //};
    public static class Activations
    {
        public static double Sigmoid(double inVal)
        {
            return 1 / (1 + Math.Exp(-inVal));
        }
    }
}
