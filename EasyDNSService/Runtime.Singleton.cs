using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    internal partial class Runtime
    {
        #region Private Static Declarations
        private static readonly object syncObject = new object();
        private static volatile Runtime instance;
        #endregion

        #region Public Static Properties
        public static Runtime Instance
        {
            get
            {
                lock (Runtime.syncObject)
                {
                    if (Runtime.instance == null)
                        Runtime.instance = new Runtime();

                    return Runtime.instance;
                }
            }
        }
        #endregion
    }
}
