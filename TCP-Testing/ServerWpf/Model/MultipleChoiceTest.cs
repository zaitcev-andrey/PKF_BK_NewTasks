namespace ServerWpf.Model
{
    /// <summary>
    /// Класс, описывающий тест на множественный выбор
    /// </summary>
    internal class MultipleChoiceTest : ITest
    {
        public string TestTypeString { get; }
        public TestTypes Type { get; }

        private static int _globalIndex = 0;
        public int Index { get; }
        public string Question { get; }
        public string Answer1 { get; }
        public string Answer2 { get; }
        public string Answer3 { get; }
        public string Answer4 { get; }
        public string Answer5 { get; }
        public string Answer6 { get; }
        public string TrueAnswer { get; }

        public MultipleChoiceTest(string _question, string _answer1, string _answer2, string _answer3, string _answer4,
            string _answer5, string _answer6, string _trueAnswer) 
        {
            Type = TestTypes.MultipleChoiceTest;
            TestTypeString = TestTypesNameString.GetNameByType(Type);

            _globalIndex++;
            Index = _globalIndex;

            Question = _question;
            Answer1 = _answer1;
            Answer2 = _answer2;
            Answer3 = _answer3;
            Answer4 = _answer4;
            Answer5 = _answer5;
            Answer6 = _answer6;
            TrueAnswer = _trueAnswer;
        }
    }

    
}
