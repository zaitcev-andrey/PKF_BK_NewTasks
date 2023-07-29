using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal class SequenceTest : ITest
    {
        public string TestTypeString { get; }
        public TestTypes Type { get; }

        private static int _globalIndex = 0;
        public int Index { get; }
        public string Question { get; }
        public string Action1 { get; }
        public string Action2 { get; }
        public string Action3 { get; }
        public string Action4 { get; }
        public string TrueActions { get; }

        public SequenceTest(string _question, string _action1, string _action2, string _action3, string _action4, string _trueActions)
        {
            Type = TestTypes.Sequence;
            TestTypeString = TestTypesNameString.GetNameByType(Type);

            _globalIndex++;
            Index = _globalIndex;

            Question = _question;
            Action1 = _action1;
            Action2 = _action2;
            Action3 = _action3;
            Action4 = _action4;

            TrueActions = _trueActions;
        }
    }
}
