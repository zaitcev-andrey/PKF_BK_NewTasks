namespace ServerWpf.Model
{
    public enum TestTypes
    {
        MultipleChoiceTest,
        Sequence
    };

    static class TestTypesNameString
    {
        public static string GetNameByType(TestTypes type)
        {
            string name = "";
            switch (type)
            {
                case TestTypes.MultipleChoiceTest:
                    name = "MultipleChoiceTest";
                    break;
                case TestTypes.Sequence:
                    name = "Sequence";
                    break;
            }
            return name;
        }
    }
}
