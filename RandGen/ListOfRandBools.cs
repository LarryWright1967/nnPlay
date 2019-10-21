using System;
using System.Threading.Tasks;

namespace RandGen
{
    public class ListOfRandBools : IDisposable
    {
        private LockListClass<bool> RandBools = new LockListClass<bool>();
        private RandStruct RandStruct1;
        private bool temp = false;
        private bool run = false;

        public ListOfRandBools()
        {
            RandStruct1 = new RandStruct(0, 1);
            GenRand();
        }

        public int Count()
        {
            return RandBools.Count();
        }

        public bool ReturnOneValue()
        {
            bool notdone = true;
            int loopcount = 0;
            while (notdone)
            {
                loopcount++;
                if (RandBools.Count() > 0)
                {
                    notdone = false;
                }
                else { System.Threading.Thread.Sleep(20); }
                if (loopcount > 100) break;
            }
            return RandBools.ExtractOne(0);
        }

        public void GenRand()
        {
            if (run == true) return;
            run = true;
            Task.Run(() =>
            {
                while (run)
                {
                    if (RandBools.Count() < 1000000) // keep 10000 entries in the bank
                    {
                        int i = RandStruct1.RandInt(1);
                        if (i == 1 || i == 0)
                        {
                            if (i == 1) { temp = true; } else { temp = false; }
                            RandBools.Add(temp);
                        }
                        System.Threading.Thread.Sleep(5);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                    }
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
