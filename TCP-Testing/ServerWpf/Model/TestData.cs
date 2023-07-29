namespace ServerWpf.Model
{
    internal class TestData
    {
        public string TestString;
        public TestTypes Type { get; }
        public int Index { get; }

        public TestData(string testString, TestTypes type, int index)
        {
            TestString = testString;
            Type = type;
            Index = index;
        }
    }
}
