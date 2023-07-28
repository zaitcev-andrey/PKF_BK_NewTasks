using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal class AllTestsModel
    {
        #region MultipleChoiceTest1
        private string _question1_MCT1 = "как дела?";
        private string _answer1_MCT1 = "ответ1";
        private string _answer2_MCT1 = "ответ2";
        private string _answer3_MCT1 = "ответ3";
        private string _answer4_MCT1 = "ответ4";
        private int _trueAnswer_MCT1 = 3;
        #endregion

        #region MultipleChoiceTest2
        private string _question1_MCT2 = "как дела2?";
        private string _answer1_MCT2 = "ответ12";
        private string _answer2_MCT2 = "ответ22";
        private string _answer3_MCT2 = "ответ32";
        private string _answer4_MCT2 = "ответ42";
        private int _trueAnswer_MCT2 = 2;


        private List<int> _answersForMultipleChoiceTests;
        #endregion

        public List<MultipleChoiceTest> AllMultipleChoiceTests { get; set; }
        //public List<MultipleChoiceTest> AllMultipleChoiceTests { get; set; }

        public AllTestsModel()
        {
            AllMultipleChoiceTests = new List<MultipleChoiceTest>();

            MultipleChoiceTest mctTest1 = new MultipleChoiceTest(_question1_MCT1, _answer1_MCT1, _answer2_MCT1, _answer3_MCT1, _answer4_MCT1, _trueAnswer_MCT1);
            MultipleChoiceTest mctTest2 = new MultipleChoiceTest(_question1_MCT2, _answer1_MCT2, _answer2_MCT2, _answer3_MCT2, _answer4_MCT2, _trueAnswer_MCT2);
            AllMultipleChoiceTests.Add(mctTest1);
            AllMultipleChoiceTests.Add(mctTest2);

            _answersForMultipleChoiceTests = new List<int>
            {
                _trueAnswer_MCT1,
                _trueAnswer_MCT2
            };
        }

        public TestData GetTestData(object selectedTest)
        {
            ITest tmpTest = selectedTest as ITest;
            StringBuilder testString = new StringBuilder();
            TestData result = null;
            if (tmpTest.Type == TestTypes.MultipleChoiceTest)
            {
                MultipleChoiceTest test = selectedTest as MultipleChoiceTest;
                testString.AppendLine($"На следующий вопрос вам нужно дать единственный ответ. Напишите цифру ответа");
                testString.AppendLine($"Вопрос: {test.Question}");
                testString.AppendLine($"Ответ 1: {test.Answer1}");
                testString.AppendLine($"Ответ 2: {test.Answer2}");
                testString.AppendLine($"Ответ 3: {test.Answer3}");
                testString.AppendLine($"Ответ 4: {test.Answer4}");

                result = new TestData(testString.ToString(), TestTypes.MultipleChoiceTest, test.Index);
            }
            return result;
        }

        public bool GetResultForTest(string answer, TestData data)
        {
            answer = answer.Trim();
            bool result = false;
            if(data.Type == TestTypes.MultipleChoiceTest)
            {
                if (_answersForMultipleChoiceTests[data.Index - 1] == int.Parse(answer))
                    result = true;
            }
            return result;
        }
    }
}
