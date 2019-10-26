using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandGen
{
    public class ListOfRandDouble_ZeroToOne : IDisposable
    {
        private LockListClass<double> RandDubVals = new LockListClass<double>();
        private RandStruct RandStruct1;
        private double temp = 0.0;
        private bool run = false;

        public ListOfRandDouble_ZeroToOne()
        {
            RandStruct1 = new RandStruct(0, 1000000000000);
            GenRand();
        }

        public int Count()
        {
            return RandDubVals.Count();
        }

        public double ReturnOneValue()
        {
            bool notdone = true;
            int loopcount = 0;
            while (notdone)
            {
                loopcount++;
                if (RandDubVals.Count() > 0)
                {
                    notdone = false;
                }
                else { System.Threading.Thread.Sleep(20); }
                if (loopcount > 100) break;
            }
            return RandDubVals.ExtractOne();
        }

        public List<double> ReturnRangeOfValues(int count)
        {
            bool notdone = true;
            int loopcount = 0;
            while (notdone)
            {
                loopcount++;
                if (RandDubVals.Count() > count)
                {
                    notdone = false;
                }
                else { System.Threading.Thread.Sleep(20); }
                if (loopcount > 100) break;
            }
            return RandDubVals.ExtractMany(count);
        }


        public void GenRand()
        {
            if (run == true) return;
            run = true;
            Task.Run(() =>
            {
                for (int i = 1; i <= 30; i++)
                {
                    Task.Run(() =>
                    {
                        while (run)
                        {
                            if (RandDubVals.Count() < 1000000) // keep 10000 entries in the bank
                        {
                                decimal d = RandStruct1.GetRand();
                                if (d <= (RandStruct1.valueRange * RandStruct1.multiplyer) && d >= 0m)
                                {
                                    temp = (double)d / 1000000000000.0;
                                    if (temp < 1.0 && temp > 0.0)
                                    {
                                        RandDubVals.Add(temp);
                                    }
                                }
                            //System.Threading.Thread.Sleep(1);
                        }
                            else
                            {
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    });
                }
            });
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    run = false;
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ListOfRandDouble_ZeroToOne()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
