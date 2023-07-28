using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal interface ITest
    {
        string TestTypeString { get; }

        TestTypes Type { get; }
    }
}
