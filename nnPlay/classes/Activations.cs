using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    public static class Activations
    {
        public static double Sigmoid(double inVal)
        {
            return 1 / (1 + Math.Exp(-inVal));
        }
    }
}
